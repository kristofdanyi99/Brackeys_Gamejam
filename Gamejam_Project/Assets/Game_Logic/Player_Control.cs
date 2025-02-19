using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float sprintSpeed = 8f;
    public float moveSpeed = 5f;
    public float jumpForce = 14f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Get player input
        movement.x = Input.GetAxisRaw("Horizontal"); //For A and D
        //movement.y = Input.GetAxisRaw("Vertical"); //For W and S (implement maybe for when used with ladders)

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = sprintSpeed;
        }

        //Translate input to player movement
        rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);
    }
}
