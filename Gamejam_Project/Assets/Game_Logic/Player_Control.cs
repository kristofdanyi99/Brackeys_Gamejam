using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public float sprintSpeed = 8f;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //Get player input
        movement.x = Input.GetAxisRaw("Horizontal"); //For A and D
        //movement.y = Input.GetAxisRaw("Vertical"); //For W and S (implement maybe for when used with ladders)
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = sprintSpeed;
        }

        //Translate input to player movement
        rb.velocity = movement * currentSpeed;
    }
}
