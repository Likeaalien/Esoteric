using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PickupManager pickup_manager;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 input_state;
    public InputAction move_action;
    int player_movement_speed;
    Vector2 player_direction;    
    public Weapon player_current_weapon;
    public GameObject rock_prefab;
    public GameObject arrow_prefab;
    void Start()
    {
        move_action.Enable();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rock_prefab = Resources.Load<GameObject>("Projectile/Rock");
        arrow_prefab = Resources.Load<GameObject>("Projectile/Arrow");

        player_movement_speed = 5;
        player_current_weapon = new MeleeWeapon(10, 0.1f, 0.1f);
    }
    void Update()
    {
        input_state = move_action.ReadValue<Vector2>();
        
        // @dev: Game uses discrete coordinates for controls. This checks whether any move_action input is currently active.
        if (input_state.sqrMagnitude != 0)
        {
            input_state.Normalize();
            player_direction = input_state;

            animator.SetFloat("Move X", input_state.x);
            animator.SetFloat("Move Y", input_state.y);    
        }

        animator.SetBool("IsRunning", input_state.sqrMagnitude > 0.01f);

        if (Input.GetKeyDown(KeyCode.C))
        {
            weapon_fire();
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
    void weapon_fire()
    {
        Weapon current_weapon = player_current_weapon;
        current_weapon.Launch((rigidbody2d.position, player_direction));
    }
    // ============================================================= \\
    //                           PICKUP                              \\
    // ============================================================= \\
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)player_direction, 1);     
    }
}
