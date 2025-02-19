using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float sprintSpeed = 8f;
    public float moveSpeed = 5f;
    public float jumpForce = 14f;
    public float climbSpeed = 4f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    
    private bool isGrounded;
    private bool isOnLadder;

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
        movement.y = Input.GetAxisRaw("Vertical"); //For W and S 

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if(isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (isOnLadder)
            {
                isOnLadder = false;
                rb.gravityScale = 12f;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = sprintSpeed;
        }

        if (isOnLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, movement.y * climbSpeed);
        }
        else
        {
            //Translate input to player movement
            rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isOnLadder = false;
            rb.gravityScale = 12f;
        }
    }
}
