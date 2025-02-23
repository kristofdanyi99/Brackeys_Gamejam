using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float sprintSpeed = 8f;
    public float moveSpeed = 5f;
    public float jumpForce = 13f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask upstairsLayer;
    public LayerMask downstairsLayer;
    
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
        movement.x = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.2f, upstairsLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.2f, downstairsLayer);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
