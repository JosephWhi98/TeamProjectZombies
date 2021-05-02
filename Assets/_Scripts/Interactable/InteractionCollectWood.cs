using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollectWood : InteractableBase
{

    public bool hasPlanks = false; 

    public override void OnInteract()
    {
        base.OnInteract();

        if (hasPlanks == false)
        {
            Debug.Log("You have collected some planks");
            hasPlanks = true; 
        } else
        {
            Debug.Log("You already have planks");
        }
        Debug.Log("Collected Wood"); 
    }
}
