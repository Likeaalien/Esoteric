using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.U2D.Animation;

public class PlayerAddRequest
{
    public string Player_Nick;
    public int Currency_Wood;
    public int Currency_Gold;
    public float Time;
}
public class Player : MonoBehaviour
{
    [SerializeField] private PickupManager pickup_manager;
    [SerializeField] private ObjectiveManager objective_manager;
    [SerializeField] private DialogueManager dialogue_manager;
    [SerializeField] private SpriteLibrary sprite_library;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 input_state;
    private int player_movement_speed;
    Vector2 player_direction;    
    Vector2 player_position;
    public Weapon player_current_weapon;
    public GameObject rock_prefab;
    public GameObject arrow_prefab;
    public GameObject equipped_weapon_prefab;
    public IInteractable last_interactable_object;
    public string player_nickname;

    // ============================================================= \\
    //                          INVENTORY                            \\
    // ============================================================= \\
    private Dictionary<int, int> player_inventory;
    const int WOOD_ID = 0;
    const int ORE_ID = 1;
    const int GOLD_ID = 2;
    public float weapon_used;
    public bool wooden_key;

     // ============================================================= \\
    //                          GET-SET                              \\
    // ============================================================= \\
    public int Change_Movement
    {
        get { return player_movement_speed; }
        set { player_movement_speed = value; }
    }
    public Vector2 Player_Position
    {
        get { return player_position; }
    }
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rock_prefab = Resources.Load<GameObject>("Prefabs/Rock");
        arrow_prefab = Resources.Load<GameObject>("Projectile/Arrow");

        player_movement_speed = 5;
        player_set_unarmed();

        weapon_used = 0;
        wooden_key = false;

        player_inventory = new();
    }
    public void Move(InputAction.CallbackContext context)
    {
        input_state = context.ReadValue<Vector2>();

        if (input_state.sqrMagnitude != 0)
        {
            input_state.Normalize();
            player_direction = input_state;

            animator.SetFloat("Move X", input_state.x);
            animator.SetFloat("Move Y", input_state.y);    
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weapon_start_attack();
        }
    }
    public void DropWeapon(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            weapon_drop();
        }
    }
    public void Interaction(InputAction.CallbackContext context)
    {
        if (context.performed && last_interactable_object != null)
        {
            interact();
        }
    }
    void Update()
    {
        // Animations
        animator.SetBool("IsRunning", input_state.sqrMagnitude > 0.01f);
   
        if (weapon_attack_started() && weapon_used + player_current_weapon.weapon_cooldown < Time.time)
        {
            weapon_end_attack();
        }
        player_position = transform.position;
    }
    void FixedUpdate()
    {
        Vector2 key_pressed = rigidbody2d.position + input_state * player_movement_speed * Time.deltaTime;  
        rigidbody2d.MovePosition(key_pressed);
    }
    // ============================================================= \\
    //                           PLAYER CURRENCY                     \\
    // ============================================================= \\

    // TODO: Update currency
    public void UpdateCurrency(int id, int amount)
    {
        if (!player_inventory.ContainsKey(id))
        {
            player_inventory[id] = amount;
        }
        else
        {
            player_inventory[id] += amount;
        }
        Debug.Log(player_inventory[id]);
    }
    public int GetCurrency(int id)
    {
        if (player_inventory.ContainsKey(id))
            return player_inventory[id];

        return 0;
    }
    // ============================================================= \\
    //                           WEAPON                              \\
    // ============================================================= \\
    void player_set_unarmed()
    {
        player_current_weapon = new MeleeWeapon(1, 0.1f, 0.1f, MeleeType.Sharp);
        ChangeSprite("Unarmed");   
    }
    bool weapon_attack_started()
    {
        return weapon_used != 0;
    }
    void weapon_start_attack()
    {
        if(weapon_attack_started())
            return;

        weapon_used = Time.time;
        animator.SetTrigger("isAttacking");
    }
    void weapon_end_attack()
    {
        player_current_weapon.Launch((rigidbody2d.position, player_direction));
        weapon_used = 0;
    }
    void weapon_drop()
    {
        if (equipped_weapon_prefab == null)
            return;
        
        float angle = Mathf.Atan2(transform.position.y, player_direction.x) * Mathf.Rad2Deg;
        Instantiate(equipped_weapon_prefab, rigidbody2d.position, Quaternion.Euler(0, 0, 0));

        player_set_unarmed();
        equipped_weapon_prefab = null; 
    }
    public void ChangeSprite(string sprite_name)
    {
        sprite_library.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>("SpriteLibrary/" + sprite_name);
    }
    // ============================================================= \\
    //                           PICKUP                              \\
    // ============================================================= \\
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectiveTrigger trigger = collision.GetComponent<ObjectiveTrigger>();
        if (trigger != null)
        {
            objective_manager.HandleTriggers(trigger);  
            return;
        }
        
        Pickup pickup = collision.GetComponent<Pickup>();
        if (pickup != null)
        {
            if(!IsWeapon(pickup.pickup_type)) 
            {
                item_interact(pickup);
                return;
            }
        }
 
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if(interactable != null)
        {
            last_interactable_object = interactable;
            interactable.CreateInteractionIcon();
            return;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if(interactable != null)
        {
            last_interactable_object = null;
            interactable.DestroyInteractionIcon();
            return;
        }
    }

    bool IsWeapon(PickupType type)
    {
        return type.ToString().StartsWith("Weapon_");
    }

    // ============================================================= \\
    //                          INTERACT                             \\
    // ============================================================= \\
    public void start_dialogue(NPCDialog data)
    {
        if (dialogue_manager.is_dialogue_active)
            return;

        dialogue_manager.dialogue_data = data;
        dialogue_manager.StartDialogue();
    }

    public void item_interact(Pickup pickup)
    {
        pickup_manager.HandlePickup(this, pickup);
    }

    public void interact()
    {
        if (last_interactable_object.CanInteract())
        {
            last_interactable_object.Interact(this);
        }
    }

    // ============================================================= \\
    //                          BACKEND                              \\
    // ============================================================= \\
    public async void SendLeaderboardData()
    {
        string url = "http://109.245.69.47:10002/Leaderboards/PlayerAdd";
        
        PlayerAddRequest data = new PlayerAddRequest
        {
            Player_Nick = player_nickname,
            Currency_Wood = GetCurrency(0),
            Currency_Gold = GetCurrency(2),
            Time = Time.time
        };
        string data_string = JsonUtility.ToJson(data);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(data_string);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            
            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();
        }
    }
    // ============================================================= \\
    //                            DEBUG                              \\
    // ============================================================= \\
    void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position + (Vector3)player_direction, 1);     
    }
}
