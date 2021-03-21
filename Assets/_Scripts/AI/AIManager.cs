using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AIManager : MonoBehaviour
{
    public enum AIType { ZOMBIE, };

    [SerializeField] AIBase[] aiPrefabs;


    public List<AIBase> baseAIComponents;
    public Transform[] aiSpawnPositions;


    public void Awake()
    {
        baseAIComponents = new List<AIBase>();
    }

    public void SpawnNewAI(AIType aiType)
    {
        Transform spawnPoint = aiSpawnPositions[Random.Range(0, aiSpawnPositions.Length)];
        SpawnNewAI(aiType, spawnPoint.position);
    }

    public void SpawnNewAI(AIType aiType, Vector3 spawnPosition)
    {
        AIBase enemyPrefab = aiPrefabs.FirstOrDefault(e => e.type == aiType);

        if (enemyPrefab)
        {
            GameObject newAIObject = Instantiate(enemyPrefab.gameObject, spawnPosition, Quaternion.identity, transform);
            baseAIComponents.Add(newAIObject.GetComponent<AIBase>());
        }
    }

    public Transform GetTarget(AIBase aiComponent)
    {
        switch (aiComponent.currentRoom)
        {
            case Room.RoomType.OUTSIDE:
                return GetOutsideTarget();
            case Room.RoomType.INSIDE:
                return GetInsideTarget();
        }

        return null;
    }

    public Transform GetInsideTarget()
    {
        return FindObjectOfType<FirstPersonMovement>().transform; //TODO - this needs to decide which player to target or if it would be best to target the generator. 
    }

    public Transform GetOutsideTarget()
    {
        //TODO - window target needs to be determined based on how many enemies are already targeting a window. 
        int index = Random.Range(0, BaseScene.Instance.enterancePortals.Length);
        return BaseScene.Instance.enterancePortals[index].transform;
    }
}
