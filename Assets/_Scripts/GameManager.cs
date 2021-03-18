using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public static GameObject playerInstance;
    public Transform[] spawnPoints;
    void Start()
    {
        if(PhotonNetwork.InRoom)
            playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length].position, Quaternion.identity);
    }

}
