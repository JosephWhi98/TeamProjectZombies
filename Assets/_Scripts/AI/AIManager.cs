using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public enum AIType { ZOMBIE, };
public class AIManager : MonoBehaviour
{
    public Transform[] aiSpawnPositions;

    private IAISpawnHandler aiSpawner;
    [Header("DebugInfo")]
    public List<AIBase> baseAIComponents;

    public int waveNumber = 0;
    public int enemySpawnAmount = 0;
    public int enemiesKilled = 0;


    public void Awake()
    {
        baseAIComponents = new List<AIBase>();
        if (!PhotonNetwork.IsMasterClient)
            enabled = false;
    }

    private void Start()
    {
        aiSpawner = GetComponent<IAISpawnHandler>();

        StartWave();
    }

    public void SpawnNewAI(AIType aiType)
    {
        Transform spawnPoint = aiSpawnPositions[Random.Range(0, aiSpawnPositions.Length)];
        baseAIComponents.Add(aiSpawner.Spawn(aiType, spawnPoint.position));
    }

    public Transform GetTarget(AIBase aiComponent)
    {
        switch (aiComponent.currentRoom)
        {
            case Room.RoomType.OUTSIDE:
                return GetOutsideTarget();
            case Room.RoomType.INSIDE:
                return GetInsideTarget(aiComponent.transform);
        }

        return null;
    }

    public Transform GetInsideTarget(Transform aiTransform)
    {
        return GameManager.instance.GetClosestPlayerTransform(aiTransform.position);
    }

    public Transform GetOutsideTarget()
    {
        //TODO - window target needs to be determined based on how many enemies are already targeting a window. 
        int index = Random.Range(0, BaseScene.Instance.enterancePortals.Length);
        return BaseScene.Instance.enterancePortals[index].entranceTarget;
    }

    public void StartWave()
    {
        waveNumber = 1;
        enemySpawnAmount = 2;
        enemiesKilled = 0;
        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnNewAI(AIType.ZOMBIE);
        }
    }

    public void NextWave()
    {
        waveNumber++;
        enemySpawnAmount += 3;
        enemiesKilled = 0;
        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnNewAI(AIType.ZOMBIE);
        }
    }

    public void Update()
    {
        if (enemiesKilled >= enemySpawnAmount)
        {
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        enemiesKilled = 0;
        yield return new WaitForSeconds(15);

        NextWave();
    }

#if UNITY_EDITOR
    [ContextMenu("TestSpawn")]
    public void SpawnZombieTest()
    {
        SpawnNewAI(AIType.ZOMBIE);
    }
#endif
}
