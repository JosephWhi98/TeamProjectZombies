using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    float HoldDuration 
    { 
        get; 
    }

    bool HoldInteract 
    { 
        get; 
    } //hold interact or click 

    bool MultipleUse 
    { 
        get; 
    }
    
    bool IsInteractable
    {
        get; 
    }

    void OnInteract(); 

}

