using NetworkingSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace NetworkingSystems
{
    public class JoinRoomByCode : MonoBehaviour
    {
        public TMP_InputField input;
        public LobbyController lobbyController;
        public void Join()
        {
            lobbyController.JoinRoom(input.text.ToUpper());
        }
    }
}