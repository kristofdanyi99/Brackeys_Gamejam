using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float sprintSpeed = 8f;
    public float moveSpeed = 5f;
    public float jumpForce = 14f;
    public float climbSpeed = 4f;
    public float ladderSpeed = 2f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    
    private bool isGrounded;
    private bool isOnLadder;
    private bool isClimbing;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Allow normal jumping when on the ground
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Allow ladder grabbing when W or S is pressed near the ladder
        if (!isOnLadder && IsTouchingLadder() && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            GrabLadder();
        }

        // Jumping off the ladder
        if (isOnLadder && Input.GetKeyDown(KeyCode.Space))
        {
            ExitLadder(movement.x, jumpForce);
        }

        // Stop climbing if no vertical input
        if (isOnLadder && movement.y == 0 && movement.x == 0)
        {
            rb.velocity = Vector2.zero;
        }

        if (isOnLadder && !IsTouchingLadder())
        {
            ExitLadder(0, 0);
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        if (isOnLadder)
        {
            rb.velocity = new Vector2(movement.x * ladderSpeed, movement.y * climbSpeed);
        }
        else
        {
            rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);
        }
    }

    private bool IsTouchingLadder()
    {
        Collider2D ladder = Physics2D.OverlapCircle(transform.position, 0.3f, ladderLayer);
        return ladder != null;
    }

    private void GrabLadder()
    {
        isOnLadder = true;
        isClimbing = true;
        rb.gravityScale = 0f; // Disable gravity
        rb.velocity = Vector2.zero; // Stop all forces
    }

    private void ExitLadder(float jumpDirection, float jumpStrength)
    {
        isOnLadder = false;
        isClimbing = false;
        rb.gravityScale = 8f; // Restore gravity
        rb.velocity = new Vector2(jumpDirection * moveSpeed, jumpStrength); // Jump in chosen direction
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            // Allow ladder grab only if pressing W or S
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                GrabLadder();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            ExitLadder(0, 0);
        }
    }
}
