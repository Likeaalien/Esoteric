using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    int movement_speed;
    public InputAction move_action;
    Vector2 input_state;
    void Start()
    {
        move_action.Enable();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        movement_speed = 10;
    }
    void Update()
    {
        input_state = move_action.ReadValue<Vector2>();
        
        // @dev: Game uses discrete coordinates for controls. This checks whether any move_action input is currently active.
        if (input_state.sqrMagnitude != 0)
        {
            input_state.Normalize();

            animator.SetFloat("Move X", input_state.x);
            animator.SetFloat("Move Y", input_state.y);    
        }
        animator.SetBool("IsRunning", input_state.sqrMagnitude > 0.01f);
    }
    void FixedUpdate()
    {
        Vector2 key_pressed = rigidbody2d.position + input_state * movement_speed * Time.deltaTime;  
        rigidbody2d.MovePosition(key_pressed);
    }
}
