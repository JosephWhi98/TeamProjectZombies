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

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            if (!photonView.IsMine)
            {
                //DISABLE PLAYER INPUT SYSTEMS
                if (TryGetComponent(out Rigidbody rb))
                    rb.isKinematic = true;
                crouchController.receiveInput = false;

                Destroy(GetComponentInChildren<AudioListener>(true));
            }
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            if (photonView.IsMine)
            {
                crouchController.CrouchStart += RPCCrouchStart;
                crouchController.CrouchEnd += RPCCrouchEnd;
            }
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
        }
    }
}