using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon;
using Photon.Pun;

public class AIBase : MonoBehaviourPun
{
    public enum States { NONE, PATH_PORT, AT_PORT, PATH_PLAYER, PATH_GENERATOR, AT_GENERATOR };

    public AIType type;
    public States currentState = States.NONE;

    public float moveSpeed;
    public float turnSpeed;
    public Transform target;
    public NavMeshAgent navMeshAgent;

    public bool hasTarget;

    public Room.RoomType currentRoom;

    public float nextPossibleAttackTime;

    private void Start()
    {
        currentRoom = Room.RoomType.OUTSIDE; //TEMP - will be determined by position in scene in future. 
    }

    public void Update()
    {
        HandleStates();

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

    public bool AttackTarget()
    {
        IDamageable damageable = (IDamageable)target.GetComponent(typeof(IDamageable));

        if (damageable != null && Time.time > nextPossibleAttackTime)
        {
            if (!damageable.IsDead())
            {
                if (currentState == States.AT_PORT)
                {
                    damageable.TakeDamage(1);
                }

                nextPossibleAttackTime = Time.time + 2f;
                return false;
            }
            else
            {
                return true;
            }
        }
        return false; 
    }

    public void HandleStates()
    {
        switch (currentState)
        {
            case States.NONE:
                target = BaseScene.Instance.aiManager.GetTarget(this);
                currentState = States.PATH_PORT;
                break;
            case States.PATH_PORT:
                if (navMeshAgent.remainingDistance < 1)
                {
                    currentState = States.AT_PORT;
                }
                break;
            case States.AT_PORT:
                //CHECK PORT HEALTH - ATTACK OR SELECT NEXT TARGET!
                if (AttackTarget())
                {
                    currentState = States.PATH_PLAYER;
                    currentRoom = Room.RoomType.INSIDE;
                    target = BaseScene.Instance.aiManager.GetTarget(this);
                }
                break;
            default:
                break;
        }
    }

}
