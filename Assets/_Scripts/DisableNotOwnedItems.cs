using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableNotOwnedItems : MonoBehaviour
{
    [SerializeField] private Behaviour[] behaviours;
    void Start()
    {
        if (TryGetComponent(out PhotonView pv))
        {
            if (!pv.IsMine)
            {
                foreach (Behaviour b in behaviours)
                    b.enabled = false;
            }
        }
        else
        {
            Destroy(this);
        }
    }

}
