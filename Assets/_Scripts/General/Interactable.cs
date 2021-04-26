using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPickupType
{
    EPT_Planks,
    EPT_Ammo,
    EPT_Health,

    EPT_Size
}

public class Interactable : MonoBehaviour
{
    //public GameObject Plank;
    //public GameObject PickupPlanksText;
    //public RaycastHit Hit;
    //public bool hasPlanks;
    public EPickupType pickupType = EPickupType.EPT_Ammo;
    public float amount = 14; 


}
