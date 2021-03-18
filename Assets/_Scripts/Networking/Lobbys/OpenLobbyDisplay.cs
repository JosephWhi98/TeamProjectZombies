using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NetworkingSystems
{
    public class OpenLobbyDisplay : MonoBehaviourPunCallbacks
    {
        public Dictionary<string, LobbyInfoPrefab> roomsList = new Dictionary<string, LobbyInfoPrefab>();
        public Transform listParent;
        public LobbyInfoPrefab prefab;
        public override void OnEnable()
        {
            base.OnEnable();
            //FillRooms

        }
        public override void OnDisable()
        {
            base.OnDisable();
            //EmptyRooms

        }
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("RoomListUpdate");
            foreach (RoomInfo r in roomList)
            {
                Debug.Log(r.Name);
                if (r.RemovedFromList)
                {
                    if (roomsList.ContainsKey(r.Name))
                        Destroy(roomsList[r.Name].gameObject);
                }
                else//addtolist
                {
                    if (roomsList.ContainsKey(r.Name))
                    {
                        roomsList[r.Name].Setup(r);
                    }
                    else
                    {
                        LobbyInfoPrefab lip = Instantiate(prefab, listParent);
                        lip.Setup(r);
                        roomsList.Add(r.Name, lip);
                    }
                }
            }
        }
    }
}
