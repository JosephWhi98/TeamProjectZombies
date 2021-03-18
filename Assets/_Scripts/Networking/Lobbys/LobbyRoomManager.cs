using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace NetworkingSystems
{
    public class LobbyRoomManager : MonoBehaviourPunCallbacks
    {
        public GameObject UI;
        public TextMeshProUGUI lobbyName;
        public Transform playerListParent;
        public PlayerListItemPrefab playerListItemPrefab;
        public Dictionary<int, PlayerListItemPrefab> playerList = new Dictionary<int, PlayerListItemPrefab>();

        public override void OnJoinedRoom()
        {
            UI.SetActive(true);
            lobbyName.SetText(PhotonNetwork.CurrentRoom.Name);
            foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
                AddPlayerToList(p);
        }
        public override void OnLeftRoom()
        {
            int[] keys = playerList.Keys.ToArray();
            foreach (int p in keys)
                RemovePlayerFromList(p);

            UI.SetActive(false);
        }
        public void LeaveRoom()
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerToList(newPlayer);
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"Player {otherPlayer.NickName} left, isActive: {!otherPlayer.IsInactive}");
            if (!otherPlayer.IsInactive)
                RemovePlayerFromList(otherPlayer.ActorNumber);
        }
        private void AddPlayerToList(Player newPlayer)
        {
            PlayerListItemPrefab go = Instantiate(playerListItemPrefab, playerListParent);
            go.Setup(newPlayer);
            playerList.Add(newPlayer.ActorNumber, go);
        }
        private void RemovePlayerFromList(int playerID)
        {
            Destroy(playerList[playerID].gameObject);
            playerList.Remove(playerID);
        }

    }
}