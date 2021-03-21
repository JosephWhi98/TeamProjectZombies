using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType { OUTSIDE, INSIDE };

    public Collider[] boundColliders;

    public void Awake()
    {
        foreach (Collider col in boundColliders)
        {
            col.isTrigger = true; 
        }
    }

    public bool CheckIfInRoom(Vector3 worldPosition)
    {
        foreach (Collider col in boundColliders)
        {
            if (col.bounds.Contains(worldPosition))
            {
                return true; 
            }
        }

        return false; 
    }
}
