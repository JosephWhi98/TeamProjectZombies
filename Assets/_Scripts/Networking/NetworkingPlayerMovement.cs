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
            gameObject.name = photonView.Owner.NickName;
            if (!photonView.IsMine)
            {   //DISABLE PLAYER INPUT SYSTEMS
                crouchController.receiveInput = false;

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
        }
        protected override void Start()
        {
            base.Start();
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

        void OnGUI()
        {
            if (photonView.IsMine)
                GUI.Label(new Rect(10, 10, 100, 20), PhotonNetwork.GetPing().ToString("F2"));
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