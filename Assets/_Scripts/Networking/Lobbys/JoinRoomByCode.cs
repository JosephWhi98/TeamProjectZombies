using NetworkingSystems;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace NetworkingSystems
{
    public class JoinRoomByCode : MonoBehaviour
    {
        public TMP_InputField input;
        public void Join()
        {
            PhotonNetwork.JoinRoom(input.text.ToUpper());
        }
    }
}