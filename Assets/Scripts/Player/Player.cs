using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    [SerializeField] private PickupManager pickup_manager;
    [SerializeField] private ObjectiveManager objective_manager;
    [SerializeField] private SpriteLibrary sprite_library;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 input_state;
    public InputAction move_action;
    int player_movement_speed;
    Vector2 player_direction;    
    public Weapon player_current_weapon;
    public GameObject rock_prefab;
    public GameObject arrow_prefab;
    public GameObject equipped_weapon_prefab;
    
    // ============================================================= \\
    //                          INVENTORY                            \\
    // ============================================================= \\
    public int wood_currency;
    public int ore_currency;
    public int gold_currency;
    public float weapon_used;
    public bool wooden_key;
    void Start()
    {
        move_action.Enable();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rock_prefab = Resources.Load<GameObject>("Prefabs/Rock");
        arrow_prefab = Resources.Load<GameObject>("Projectile/Arrow");

        player_movement_speed = 5;
        player_set_unarmed();

        weapon_used = 0;
        wooden_key = false;

        ore_currency = 11;
        gold_currency = 11;
    }
    void Update()
    {
        // Animations
        animator.SetBool("IsRunning", input_state.sqrMagnitude > 0.01f);

        // Player direction
        input_state = move_action.ReadValue<Vector2>();
        if (input_state.sqrMagnitude != 0)
        {
            input_state.Normalize();
            player_direction = input_state;

            animator.SetFloat("Move X", input_state.x);
            animator.SetFloat("Move Y", input_state.y);    
        }

        // Input key
        if (Input.GetKeyDown(KeyCode.C))
        {
            weapon_start_attack();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            weapon_drop();
        }
        if (weapon_attack_started() && weapon_used + player_current_weapon.weapon_cooldown < Time.time)
        {
            weapon_end_attack();
        }
    }
    void FixedUpdate()
    {
        Vector2 key_pressed = rigidbody2d.position + input_state * player_movement_speed * Time.deltaTime;  
        rigidbody2d.MovePosition(key_pressed);
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
        Instantiate(equipped_weapon_prefab, rigidbody2d.position + 2*player_direction, Quaternion.Euler(0, 0, angle));

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
            if (IsWeapon(pickup.pickup_type) && equipped_weapon_prefab != null)
                return;   

            pickup_manager.HandlePickup(this, pickup);
            return;   
        }
    }

    bool IsWeapon(PickupType type)
    {
        return type.ToString().StartsWith("Weapon_");
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
