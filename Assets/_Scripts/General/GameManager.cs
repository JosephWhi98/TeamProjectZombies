﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public static GameObject playerInstance;
    public Transform[] spawnPoints;

    //ActorNumber, Details.
    public Dictionary<int, PlayerDetails> players = new Dictionary<int, PlayerDetails>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    void Start()
    {
        if (PhotonNetwork.InRoom)
            playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length].position, Quaternion.identity);
    }

    public void AddPlayer(Player owner, Transform t)
    {
        if (!players.ContainsKey(owner.ActorNumber))
            players.Add(owner.ActorNumber, new PlayerDetails() { username = owner.NickName, transform = t });
        Debug.Log("New Player Added: " + owner.NickName, t.gameObject);
    }

    public Transform GetClosestPlayerTransform(Vector3 worldPosition)
    {
        float closestDistance = float.MaxValue;
        Transform closestTransform = null;


        foreach (KeyValuePair<int, PlayerDetails> kvp in players)
        {
            float dist = Vector3.Distance(worldPosition, kvp.Value.transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestTransform = kvp.Value.transform;
            }
        }

        return closestTransform;
    }
    public override void OnPlayerLeftRoom(Player p)
    {
        base.OnPlayerLeftRoom(p);
        if (players.TryGetValue(p.ActorNumber, out PlayerDetails v))
        {
            Debug.Log("Player " + v.username + " has left the lobby");
            Destroy(v.transform.gameObject);
            players.Remove(p.ActorNumber);
        }
    }
}
public class PlayerDetails
{
    public string username;
    public Transform transform;
}
