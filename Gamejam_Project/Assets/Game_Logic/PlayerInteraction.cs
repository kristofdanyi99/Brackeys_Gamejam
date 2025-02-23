using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable_Object currentInteractable;
    private Interactable_Object heldObject;
    public Transform holdPosition;
    public Transform dropPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject != null)
            {
                heldObject.Interact(holdPosition, dropPosition);
                heldObject = null;
            }
            else if (currentInteractable != null)
            {
                currentInteractable.Interact(holdPosition, dropPosition);
                heldObject = currentInteractable;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.CompareTag("Interactable"))
        {
            currentInteractable = collison.GetComponent<Interactable_Object>();

            if(heldObject == currentInteractable)
            {
                currentInteractable = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            currentInteractable = null;
        }
    }
}
