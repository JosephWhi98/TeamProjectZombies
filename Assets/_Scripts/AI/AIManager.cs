using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AIManager : MonoBehaviour
{
    public enum AIType { ZOMBIE, };

    public List<AIBase> baseAIComponents;

    [SerializeField] AIBase[] aiPrefabs;

    public void Awake()
    {
        baseAIComponents = new List<AIBase>();
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

    //returns the target for the AI based on state and location. This shoudl only be used by the host. 
    public Transform GetTarget(AIBase aiComponent)
    {
        //If outside, target a window.
        //If inside, target a player or target the generator (need rules for this??) 
        return transform; //temp
    }
}
