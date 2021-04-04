using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkingSystems
{
    public class NetworkingPlayerMovement : NetworkedTransform
    {
        public Crouch crouchController;
        public GameObject characterMeshFull;
        public GameObject characterMeshFPS;

        public NetworkedAnimatorView networkedAnimator; 

        protected override void OnEnable()
        {
            base.OnEnable();

            if (!photonView.IsMine)
            {
                //DISABLE PLAYER INPUT SYSTEMS
                //if (TryGetComponent(out Rigidbody rb))
                    //rb.isKinematic = true;
                crouchController.receiveInput = false;

                AudioListener al = GetComponentInChildren<AudioListener>(true);
                if (al)
                    Destroy(al);

                characterMeshFull.SetActive(true);
                characterMeshFPS.SetActive(false);

            }
            else //(photonView.IsMine)
            {
                characterMeshFull.SetActive(false);
                characterMeshFPS.SetActive(true);
                crouchController.CrouchStart += RPCCrouchStart;
                crouchController.CrouchEnd += RPCCrouchEnd;
            }

            GameManager.instance.AddPlayer(photonView.Owner, gameObject.transform);
               
        }
        private void OnDisable()
        {
            if (photonView.IsMine)
            {
                crouchController.CrouchStart -= RPCCrouchStart;
                crouchController.CrouchEnd -= RPCCrouchEnd;
            }
        }
        private void RPCCrouchStart()
        {
            photonView.RPC("SetIsCrouching", RpcTarget.Others, true);
        }
        private void RPCCrouchEnd()
        {
            photonView.RPC("SetIsCrouching", RpcTarget.Others, false);
        }

        [PunRPC]
        public void SetIsCrouching(bool v)
        {
            crouchController.remoteCrouchToggle = v;
            networkedAnimator.SetAnimatorBool("Crouched", v);
        }
    }
}