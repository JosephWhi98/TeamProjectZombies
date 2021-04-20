using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkingSystems
{
    public class NetworkedTransform : MonoBehaviourPun, IPunObservable
    {
        public Behaviour[] componetsToTurnOff;
        protected Vector3 networkPosition;
        protected Vector3 networkDirection;

        protected Vector3 storedPosition;

        protected Quaternion networkRotation;
        protected float distanceDelta;
        protected float angleDelta;
        protected float lastUpdateTime;

        public bool interpolate = true;
        public bool extrapolate = true;

        protected bool firstRecieve = true;

        protected virtual void Start()
        {
            if (!photonView.IsMine)
            {
                //DISABLE INPUT SYSTEMS;
                foreach (Behaviour c in componetsToTurnOff)
                {
                    c.enabled = false;
                }
            }
        }

        protected virtual void OnEnable()
        {
            firstRecieve = true;
        }
        protected virtual void Update()
        {
            if (photonView.IsMine)
            {
                return;
            }


            if (interpolate)
            {
                transform.position = Vector3.Lerp(storedPosition, networkPosition, (Time.time - lastUpdateTime) * PhotonNetwork.SerializationRate); //Time.deltaTime * PhotonNetwork.SerializationRate); //(1.0f / PhotonNetwork.SerializationRate));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, networkRotation, angleDelta * (1.0f / PhotonNetwork.SerializationRate));
            }
            else
            {
                //testing purposes only, never disable interpolation
                transform.position = networkPosition;
                transform.rotation = networkRotation;
            }
        }

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
                lastUpdateTime = Time.time;
                storedPosition = transform.position;
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
                        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));// *2.0f;swear this is needed
                        networkPosition += networkDirection * lag;
                    }
                    distanceDelta = Vector3.Distance(transform.position, networkPosition);
                }

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

        public static Vector3 Lerp(Vector3 v0, Vector3 v1, float t)
        {
            return (1 - t) * v0 + t * v1;

        }
    }
}