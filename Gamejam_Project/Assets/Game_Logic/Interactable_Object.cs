using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    public string interactionMessage = "Press E to interact";
    private Transform playerTransform;
    private bool isPickedUp = false;


    public virtual void Interact(Transform holdPosition, Transform dropPosition)
    {
        Debug.Log("Interacted with " + gameObject.name);
        if (!isPickedUp)
        {
            PickUp(holdPosition);
        }
        else
        {
            Drop(dropPosition);
        }
    }

    private void PickUp(Transform holdPosition)
    {
        transform.SetParent(holdPosition);

        transform.localPosition = Vector3.zero;

        isPickedUp = true;
        Debug.Log("Picked up " + gameObject.name);
    }

    private void Drop(Transform dropPosition)
    {
        transform.SetParent(null);

        transform.position = dropPosition.position;

        isPickedUp = false;
        Debug.Log("Dropped " + gameObject.name);
    }
}
