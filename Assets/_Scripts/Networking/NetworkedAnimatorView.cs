using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;


public class NetworkedAnimatorView : MonoBehaviourPun
{
    public Animator animator;
   
    public void TriggerAnimaton(string trigger)
    {
        photonView.RPC("AnimateTrigger", RpcTarget.All, trigger);
    }

    [PunRPC]
    public void AnimateTrigger(string s)
    {
        animator.SetTrigger(s);
    }
}
