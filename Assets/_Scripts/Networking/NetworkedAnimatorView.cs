using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;


public class NetworkedAnimatorView : MonoBehaviourPun
{
    public Animator animator;

    public void AnimatorBool(string name, bool value)
    {
        photonView.RPC("SetAnimatorBool", RpcTarget.All, name, value);
    }

    [PunRPC]
    public void SetAnimatorBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void TriggerAnimaton(string trigger, bool onlyOthers = false)
    {
        photonView.RPC("AnimateTrigger", onlyOthers ? RpcTarget.Others : RpcTarget.All, trigger);
    }


    [PunRPC]
    public void AnimateTrigger(string s)
    {
        animator.SetTrigger(s);
    }
}
