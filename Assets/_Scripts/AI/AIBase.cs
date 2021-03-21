using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon;
using Photon.Pun;

public class AIBase : MonoBehaviourPun, IPunObservable
{
    public AIManager.AIType type;

    public float moveSpeed;
    public float turnSpeed;
    public Transform target;
    public NavMeshAgent navMeshAgent;

    public bool hasTarget;

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


    public void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            navMeshAgent.enabled = false; 
        }
    }


    public void Update()
    {
        if (PhotonNetwork.IsMasterClient && target != null)
        {
            navMeshAgent.destination = target.position;
            navMeshAgent.speed = moveSpeed;
        }
        else
        {
            navMeshAgent.speed = 0; 
        }

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
