using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkingSystems
{
    public class LobbyController : MonoBehaviourPunCallbacks
    {
        public GameObject UI;
        void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master");
            PhotonNetwork.JoinLobby();
        }
        public void QuickJoin()
        {
            PhotonNetwork.JoinRandomRoom();
        }
        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(RandomLobbyName.GetRandomRoomName(), new RoomOptions
            {
                MaxPlayers = 4,
                IsVisible = false
            });
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, Calling CreateRoom().\n" + message);
            CreateRoom();
        }
        public override void OnJoinedRoom()
        {
            UI.SetActive(false);
        }
        public override void OnLeftRoom()
        {
            UI.SetActive(true);
        }

    }
    public static class RandomLobbyName
    {
        public static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static string GetRandomRoomName(int length = 6)
        {
            string s = "";
            for (int i = 0; i < length; i++)
                s += alphabet[Random.Range(0, alphabet.Length)];
            return s;
        }
    }
}