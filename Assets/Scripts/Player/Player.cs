using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    [SerializeField] private PickupManager pickup_manager;
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
    public int wood_currency;
    void Start()
    {
        move_action.Enable();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rock_prefab = Resources.Load<GameObject>("Prefabs/Rock");
        arrow_prefab = Resources.Load<GameObject>("Projectile/Arrow");

        player_movement_speed = 5;
        player_set_unarmed();

        wood_currency = 0;
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
            weapon_fire();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            weapon_drop();    
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
    void weapon_fire()
    {
        animator.SetTrigger("isAttacking");
        Weapon current_weapon = player_current_weapon;
        current_weapon.Launch((rigidbody2d.position, player_direction));
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
        if (equipped_weapon_prefab != null)
            return;

        Pickup pickup = collision.GetComponent<Pickup>();
        if (pickup != null)
        {
            pickup_manager.HandlePickup(this, pickup);
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
