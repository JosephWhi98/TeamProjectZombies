using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePort : InteractableBase 
{

    public Port port; 

    public override void OnInteract()
    {
        base.OnInteract();

        if (port.healthComponent.Health < 3)
        {
            port.Repair();
        }

        Debug.Log("Repaired Port"); //code to repair port 
    }
}
