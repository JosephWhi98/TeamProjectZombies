using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollectWood : InteractableBase
{

    public override void OnInteract()
    {
        base.OnInteract();
        Debug.Log("Collected Wood"); 
    }
}
