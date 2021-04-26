using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable 
{
    public float holdDuration = 1.0f;
    public bool holdInteract = true;
    public bool multipleUse = false;
    public bool isInteractable = true;

    public float HoldDuration => holdDuration;

    public bool HoldInteract => holdInteract;
    public bool MultipleUse => multipleUse;
    public bool IsInteractable => isInteractable;

    public virtual void OnInteract()
    {
        Debug.Log("INTERACTED: " +  gameObject.name);
    }
}
