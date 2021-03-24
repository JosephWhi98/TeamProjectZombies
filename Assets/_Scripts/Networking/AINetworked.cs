using NetworkingSystems;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINetworked : NetworkedTransform
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("PhotonView IsMine: " + photonView.IsMine, gameObject);
    }
}
