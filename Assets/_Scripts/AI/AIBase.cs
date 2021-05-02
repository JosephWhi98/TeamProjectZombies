﻿using System.Collections;
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
    public NetworkedAnimatorView animatorNetworked;

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
        }
    }

    //Used to match the speed to the motion of the animation. 
    void OnAnimatorMove()
    {
        navMeshAgent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
    }

    //Handle the AI states. This runs each frame and determines the current behaviour. 
    public void HandleStates()
    {
        switch (currentState)
        {
            case States.NONE:
                //Starting/Fallback state, get target based on position in scene. 
                target = BaseScene.Instance.aiManager.GetTarget(this);
                animatorNetworked.AnimatorBool("Walking", false);
                currentState = States.PATH_PORT;
                break;
            case States.PATH_PORT:
                //Path to entry portal.
                animatorNetworked.AnimatorBool("Walking", true);
                if (navMeshAgent.remainingDistance < 1)
                {
                    nextPossibleAttackTime = Time.time + 2f;
                    currentState = States.AT_PORT;
                }
                break;
            case States.AT_PORT:
                //Check port health, if > 0 attack, else enter the room and select next target. 
                animatorNetworked.AnimatorBool("Walking", false);
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
                //Path find towards target player. 
                animatorNetworked.AnimatorBool("Walking", true);
                if (navMeshAgent.remainingDistance < 1)
                {
                    currentState = States.AT_PLAYER;
                }
                break;
            case States.AT_PLAYER:
                //Attack the target player. 
                animatorNetworked.AnimatorBool("Walking", false);
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

    //Attack the current target. 
    public bool AttackTarget()
    {
        if (target == null || target.gameObject.activeInHierarchy == false)
        {
            Debug.Log("Target no longer exists");
            target = BaseScene.Instance.aiManager.GetTarget(this);
            currentState = States.PATH_PLAYER;
            return false;//find new target??
        }

        IDamageable damageable = (IDamageable)target.GetComponent(typeof(IDamageable));

        if (damageable == null)
            damageable = (IDamageable)(target.parent.GetComponent(typeof(IDamageable)));


        if (damageable != null && Time.time > nextPossibleAttackTime)
        {
            if (!damageable.IsDead())
            {
                animatorNetworked.TriggerAnimaton("Attack");

                if (currentState == States.AT_PORT || currentState == States.AT_PLAYER)
                {
                    StartCoroutine(DelayedAttack(1, damageable, 1));
                }

                nextPossibleAttackTime = Time.time + 4f;
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    //Delayed attack. Used to make attack timings match the animations.
    public IEnumerator DelayedAttack(float delayTime, IDamageable damageable, int damage)
    {
        yield return new WaitForSeconds(delayTime);

        if (navMeshAgent.remainingDistance < 1f)
            damageable.TakeDamage(damage);
    }
}
