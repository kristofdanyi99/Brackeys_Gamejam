using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair_Mechanic : MonoBehaviour
{
    private int choice = 0;

    public Transform downstairsCheck;
    public LayerMask downstairsLayer;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public LayerMask intersectionLayer;

    private bool isDownstairs;
    private bool isGrounded;


    void Update()
    {
        Debug.Log(choice);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Allow player to make a choice at the intersection
        if (IsTouchingIntersection())
        {
            if (verticalInput > 0)
            {
                choice = 1;
            }
            else if (verticalInput < 0)
            {
                choice = -1;
            }
        }
        else
        {
            choice = 0;
        }
        isDownstairs = Physics2D.OverlapCircle(downstairsCheck.position, 0.2f, downstairsLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isDownstairs && choice < 1 || isGrounded && choice < 1)
        {
            DisableCollidersByTag("Stairs_Up");
        }
        else
        {
            EnableCollidersByTag("Stairs_Up");
        }
    }

    private bool IsTouchingIntersection()
    {
        return Physics2D.OverlapCircle(transform.position, 0.3f, intersectionLayer) != null;
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Intersection") && choice == -1)
        {
            DisableCollidersByTag("Stairs_Up");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Intersection"))
        {
            EnableCollidersByTag("Stairs_Up");
            choice = 0;
        }
    }

    void DisableCollidersByTag(string tagName)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName); // Find all objects with the tag

        foreach (GameObject obj in objects)
        {
            PolygonCollider2D collider = obj.GetComponent<PolygonCollider2D>(); // Get BoxCollider2D
            if (collider != null)
            {
                collider.enabled = false; // Disable the collider
            }
        }
    }

    void EnableCollidersByTag(string tagName)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName); // Find all objects with the tag

        foreach (GameObject obj in objects)
        {
            PolygonCollider2D collider = obj.GetComponent<PolygonCollider2D>(); // Get BoxCollider2D
            if (collider != null)
            {
                collider.enabled = true; 
            }
        }
    }


}
