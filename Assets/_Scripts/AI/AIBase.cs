using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon;
using Photon.Pun;

public class AIBase : MonoBehaviourPun
{
    public enum States { NONE, PATH_PORT, AT_PORT, PATH_PLAYER, AT_PLAYER, PATH_GENERATOR, AT_GENERATOR };

    public AIType type;
    public States currentState = States.NONE;

    public float moveSpeed;
    public float turnSpeed;
    public Transform target;
    public NavMeshAgent navMeshAgent;
    public Animator animator; 

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
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    void OnAnimatorMove()
    { 
        navMeshAgent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
    }

    public bool AttackTarget()
    {
        IDamageable damageable = (IDamageable)target.GetComponent(typeof(IDamageable));

        if(damageable == null)
            damageable = (IDamageable)(target.parent.GetComponent(typeof(IDamageable)));


        if (damageable != null && Time.time > nextPossibleAttackTime)
        {
            Debug.Log("ATTACK?");
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
                navMeshAgent.speed = moveSpeed;
                if (navMeshAgent.remainingDistance < 1)
                {
                    nextPossibleAttackTime = Time.time + 2f;
                    currentState = States.AT_PORT;
                }
                break;
            case States.AT_PORT:
                //CHECK PORT HEALTH - ATTACK OR SELECT NEXT TARGET!
                navMeshAgent.speed = 0;
                if (AttackTarget())
                {
                    Port port = target.GetComponentInParent<Port>();
                    if (port)
                    {
                        navMeshAgent.Warp(new Vector3(port.exitTarget.position.x, transform.position.y, port.exitTarget.position.z));
                    }

                    currentState = States.PATH_PLAYER;
                    currentRoom = Room.RoomType.INSIDE;
                    target = BaseScene.Instance.aiManager.GetTarget(this);
                }
                break;
            case States.PATH_PLAYER:
                navMeshAgent.speed = moveSpeed;
                if (navMeshAgent.remainingDistance < 1)
                {
                    currentState = States.AT_PLAYER;
                }
                break;
            case States.AT_PLAYER:
                navMeshAgent.speed = 0;
                if (AttackTarget())
                {
                    currentState = States.PATH_PLAYER;
                    currentRoom = Room.RoomType.INSIDE;
                    target = BaseScene.Instance.aiManager.GetTarget(this);
                }
                else if (navMeshAgent.remainingDistance > 1)
                {
                    currentState = States.PATH_PLAYER;
                }
                break;
            default:
                break;
        }
    }

}
