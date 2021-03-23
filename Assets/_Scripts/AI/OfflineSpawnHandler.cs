using System.Linq;
using UnityEngine;

public class OfflineSpawnHandler : MonoBehaviour, IAISpawnHandler
{
    [SerializeField] AIBase[] aiPrefabs;
    public AIBase Spawn(AIType aiType, Vector3 position)
    {
        AIBase enemyPrefab = aiPrefabs.FirstOrDefault(e => e.type == aiType);

        if (enemyPrefab)
        {
            AIBase newAIObject = Instantiate(enemyPrefab, position, Quaternion.identity, transform);
            return newAIObject;
        }

        Debug.LogError("Unable to find prefab matching this type: " + aiType);
        return null;
    }

}
