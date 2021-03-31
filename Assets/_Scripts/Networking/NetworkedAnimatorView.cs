using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedAnimatorView : MonoBehaviourPun, IPunObservable
{
    public Animator animator;

    public List<string> animatorTriggers;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            foreach (string s in animatorTriggers)
            {
                stream.SendNext(animatorTriggers[0]);
                animator.SetTrigger(s);
            }

            animatorTriggers.Clear();
        }
        else
        {
            string trigger = (string)stream.ReceiveNext();

            if (!string.IsNullOrEmpty(trigger))
                animator.SetTrigger(trigger);
        }
    }


    public void TriggerAnimaton(string trigger)
    {
        animatorTriggers.Add(trigger);
    }
}
