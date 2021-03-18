using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NetworkingSystems
{
    public class LobbyInfoPrefab : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI roomName;
        [SerializeField]
        private TextMeshProUGUI playerCount;
        [SerializeField]
        private Button button;
        private RoomInfo myRoom;
        public void Setup(RoomInfo room)
        {
            myRoom = room;
            roomName.SetText(room.Name);
            playerCount.SetText($"{room.PlayerCount}/{room.MaxPlayers}");
            button.interactable = room.PlayerCount < room.MaxPlayers; 
        }
        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(myRoom.Name);
        }
    }
}