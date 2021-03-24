﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AIType { ZOMBIE, };
public class AIManager : MonoBehaviour
{
    public Transform[] aiSpawnPositions;

    private IAISpawnHandler aiSpawner;
    [Header("DebugInfo")]
    public List<AIBase> baseAIComponents;
    public void Awake()
    {
        baseAIComponents = new List<AIBase>();
    }

    private void Start()
    {
        aiSpawner = GetComponent<IAISpawnHandler>();
    }

    public void SpawnNewAI(AIType aiType)
    {
        Transform spawnPoint = aiSpawnPositions[Random.Range(0, aiSpawnPositions.Length)];
        baseAIComponents.Add( aiSpawner.Spawn(aiType, spawnPoint.position) );
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

#if UNITY_EDITOR
    [ContextMenu("TestSpawn")]
    public void SpawnZombieTest()
    {
        SpawnNewAI(AIType.ZOMBIE);
    }
#endif
}
