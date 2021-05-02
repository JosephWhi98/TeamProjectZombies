using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePort : InteractableBase 
{
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log("Repaired Port"); //code to repair port 
    }
}
