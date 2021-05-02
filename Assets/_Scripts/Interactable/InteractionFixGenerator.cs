using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionFixGenerator : InteractableBase
{
    public Generator generator; 

    public override void OnInteract()
    {
        base.OnInteract();

        if (generator.healthComponent.Health < 3)
        {
            generator.Repair();
            Debug.Log("Generator Fixed");
        }

        Debug.Log("Interacted with generator"); //code to repair generator
    }
}
