using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingPlayerMovement : MonoBehaviourPun, IPunObservable
{
    public Crouch crouchController;
    private Vector3 networkPosition;
    private Vector3 networkDirection;

    private Vector3 storedPosition;

    private Quaternion networkRotation;
    private float distanceDelta;
    private float angleDelta;

    public bool interpolate = true;
    public bool extrapolate = true;

    private bool firstRecieve = true;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            networkDirection = transform.position - storedPosition;//consider more up to date velocity as currently overshoots when jumping due to extrapolation
            storedPosition = transform.position;
            stream.SendNext(transform.position);
            stream.SendNext(networkDirection);
            stream.SendNext(transform.rotation);
        }
        else
        {

            //MOVEMENT
            networkPosition = (Vector3)stream.ReceiveNext();
            networkDirection = (Vector3)stream.ReceiveNext();

            if (firstRecieve)
            {
                transform.position = networkPosition;
                distanceDelta = 0.0f;
            }
            else
            {
                if (extrapolate)
                {
                    float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                    networkPosition += networkDirection * lag;
                }
                distanceDelta = Vector3.Distance(transform.position, networkPosition);
            }

            //ROTATION
            networkRotation = (Quaternion)stream.ReceiveNext();
            if (firstRecieve)
            {
                angleDelta = 0f;
                transform.rotation = networkRotation;
            }
            else
            {
                angleDelta = Quaternion.Angle(transform.rotation, networkRotation);
            }


            if (firstRecieve)
                firstRecieve = false;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            //DISABLE PLAYER INPUT SYSTEMS;
            GetComponent<FirstPersonMovement>().enabled = false; //Maybe destroy these 3
            GetComponentInChildren<FirstPersonLook>().enabled = false;
            GetComponent<Jump>().enabled = false; 

            GetComponent<Rigidbody>().isKinematic = true;
            crouchController.receiveInput = false;
            //DISABLE PLAYER CAMERA ETC.
            Transform cam = transform.Find("FirstPersonCamera");
            cam.GetComponent<Camera>().enabled = false;
            Destroy(cam.GetComponent<AudioListener>());
        }
    }
    void OnEnable()
    {
        firstRecieve = true;
        if (photonView.IsMine)
        {
            crouchController.CrouchStart += RPCCrouchStart;
            crouchController.CrouchEnd += RPCCrouchEnd;
        }
    }
    void OnDisable()
    {
        if (photonView.IsMine)
        {
            crouchController.CrouchStart -= RPCCrouchStart;
            crouchController.CrouchEnd -= RPCCrouchEnd;
        }
    }
    void RPCCrouchStart()
    {
        photonView.RPC("SetIsCrouching", RpcTarget.Others, true);
    }
    void RPCCrouchEnd()
    {
        photonView.RPC("SetIsCrouching", RpcTarget.Others, false);
    }

    [PunRPC]
    public void SetIsCrouching(bool v)
    {
        crouchController.remoteCrouchToggle = v;
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            return;
        }
        if (interpolate)
        {
            transform.position = Vector3.MoveTowards(transform.position, networkPosition, distanceDelta * (1.0f / PhotonNetwork.SerializationRate));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, networkRotation, angleDelta * (1.0f / PhotonNetwork.SerializationRate));
        }
        else
        {
            //testing purposes only, never disable interpolation
            transform.position = networkPosition;
            transform.rotation = networkRotation;
        }
    }
}
