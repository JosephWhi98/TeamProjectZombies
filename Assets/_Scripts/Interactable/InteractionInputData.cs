using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionsSystem/InputData")]
public class InteractionInputData : ScriptableObject 
{
    public bool interactedClicked;
    public bool interactedRelease;

    public bool InteractedClicked
    {
        get => interactedClicked;
        set => interactedClicked = value; 
    }

    public bool InteractedRelease
    {
        get => interactedRelease;
        set => interactedRelease = value; 
    }

    public void Reset()
    {
        interactedClicked = false;
        interactedRelease = false; 
    }
    
}
