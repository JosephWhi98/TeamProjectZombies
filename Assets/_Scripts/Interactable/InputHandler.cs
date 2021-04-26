using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public InteractionInputData interactionInputData; 
    // Start is called before the first frame update
    void Start()
    {
        interactionInputData.Reset(); 
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractionInputData(); 
    }

    void GetInteractionInputData()
    {
        interactionInputData.interactedClicked = Input.GetKeyDown(KeyCode.E);
        interactionInputData.interactedRelease = Input.GetKeyUp(KeyCode.E);
    }
}
