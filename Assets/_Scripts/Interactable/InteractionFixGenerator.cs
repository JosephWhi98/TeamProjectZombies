using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionFixGenerator : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log("Repaired the generator"); //code to repair generator
    }
}
