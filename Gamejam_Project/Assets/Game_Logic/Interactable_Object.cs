using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    public string interactionMessage = "Press E to interact";

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
