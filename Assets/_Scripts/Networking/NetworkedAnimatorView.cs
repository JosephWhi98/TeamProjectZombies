using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;


public class NetworkedAnimatorView : MonoBehaviourPun
{
    public Animator animator;

    public Animator fpAnimator;
    public Animator tpAnimator;
    public GameObject arms;
    public GameObject character;

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

    public void SetThirdPerson()
    {
        animator = tpAnimator;
        arms.SetActive(false);
        character.SetActive(true);
    }
    public void SetFirstPerson()
    {
        animator = fpAnimator;
        arms.SetActive(true);
        character.SetActive(false);
    }
}
