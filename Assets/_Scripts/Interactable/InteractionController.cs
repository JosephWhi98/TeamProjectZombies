using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public InteractionInputData interactionInputData;
    public InteractionData interactionData;
    public bool interacting;
    public float holdTime = 0f;

    public float rayDistance;
    public float raySphereRadius;
    public LayerMask interactableLayer;

    private Camera m_cam;

    void Awake()
    {
        m_cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }

    void CheckForInteractable()
    {
        Ray _ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
        RaycastHit _hitInfo;

        bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, interactableLayer);

        if(_hitSomething)
        {
            InteractableBase _interactable = _hitInfo.transform.GetComponent<InteractableBase>(); 
            if(_interactable != null)
            {
                if(interactionData.IsEmpty())
                {
                    interactionData.Interactable = _interactable; 
                }
                else
                {
                    if(!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable; 
                    }
                }
            }
        }
        else
        {
            interactionData.ResetData(); 
        }

        Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, _hitSomething ? Color.green : Color.red); 
    }

    void CheckForInteractableInput()
    {
        if (interactionData.IsEmpty())
        {
            return;
        }
        if (interactionInputData.InteractedClicked)
        {
            interacting = true;
            holdTime = 0f; 
        }
        if (interactionInputData.InteractedRelease)
        {
            interacting = false;
            holdTime = 0f; 
        }
        if (interacting)
        {
            if (!interactionData.Interactable.IsInteractable)
            {
                return;
            }
            if(interactionData.Interactable.HoldInteract)
            {
                holdTime += Time.deltaTime; 
                if(holdTime >= interactionData.Interactable.HoldDuration)
                {
                    interactionData.Interact();
                    interacting = false; 
                }
            }
            else
            {
                interactionData.Interact();
                interacting = false; 
            }
        }
    }
}
