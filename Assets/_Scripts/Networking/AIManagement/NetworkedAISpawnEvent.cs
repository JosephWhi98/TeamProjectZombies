using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkedAISpawnEvent : MonoBehaviour, IAISpawnHandler
{
    [SerializeField] PhotonView photonView;
    [SerializeField] AIBase[] aiPrefabs;
    public AIBase Spawn(AIType aiType, Vector3 position)
    {
        AIBase enemyPrefab = aiPrefabs.FirstOrDefault(e => e.type == aiType);

        if (enemyPrefab)
        {
            GameObject newAIObject = PhotonNetwork.Instantiate(enemyPrefab.name, position, Quaternion.identity);
            newAIObject.transform.parent = transform;
            return newAIObject.GetComponent<AIBase>();
        }

        Debug.LogError("Unable to find prefab matching this type: " + aiType);
        return null;
    }
}
