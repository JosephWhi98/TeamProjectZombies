using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon;
using Photon.Pun;

public class AIBase : MonoBehaviourPun
{
    public AIManager.AIType type;

    public float moveSpeed;
    public float turnSpeed;
    public Transform target;
    public NavMeshAgent navMeshAgent;

    public bool hasTarget;

    public Room.RoomType currentRoom;

    private void Start()
    {
        currentRoom = Room.RoomType.INSIDE; //TEMP - will be determined by position in scene in future. 
        target = BaseScene.Instance.aiManager.GetTarget(this);
    }

    public void Update()
    {
        if (target != null)
        {
            navMeshAgent.destination = target.position;
            navMeshAgent.speed = moveSpeed;
        }
        else
        {
            navMeshAgent.speed = 0; 
        }
    }

}
