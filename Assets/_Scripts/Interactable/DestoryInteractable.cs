using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryInteractable : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();

        Destroy(gameObject); //apply logic for interactable
    }

}
