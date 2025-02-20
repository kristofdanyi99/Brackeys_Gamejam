using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_Mechanic : MonoBehaviour
{
    public float climbSpeed = 4f;
    public float ladderSpeed = 2f;

    private Rigidbody2D rb;
    private bool isOnLadder;
    private bool isClimbing;

    public LayerMask ladderLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");


        // Allow ladder grabbing when W or S is pressed near the ladder
        if (!isOnLadder && IsTouchingLadder() && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            GrabLadder();
        }

        // Jumping off the ladder
        if (isOnLadder && Input.GetKeyDown(KeyCode.Space))
        {
            ExitLadder();
        }

        // Stop climbing if no vertical input
        if (isOnLadder && verticalInput == 0)
        {
            rb.velocity = Vector2.zero;
        }

        if (isOnLadder && !IsTouchingLadder())
        {
            ExitLadder();
        }
    }
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (isOnLadder)
        {
            rb.velocity = new Vector2(horizontalInput * ladderSpeed, verticalInput * climbSpeed);
        }
    }
    private bool IsTouchingLadder()
    {
        return Physics2D.OverlapCircle(transform.position, 0.3f, ladderLayer) != null;
    }

    private void GrabLadder()
    {
        isOnLadder = true;
        isClimbing = true;
        rb.gravityScale = 0f; // Disable gravity
        rb.velocity = Vector2.zero;
    }

    private void ExitLadder()
    {
        isOnLadder = false;
        isClimbing = false;
        rb.gravityScale = 8f; // Restore gravity
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
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
            ExitLadder();
        }
    }
}
