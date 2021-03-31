using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Port : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;

    public bool Open { get { return IsDead(); } }

    public Transform entranceTarget;
    public Transform exitTarget;


    public void Start() 
    {
        healthComponent = GetComponent<HealthComponent>();
    }


    public void TakeDamage(int damage = 1)
    {
        healthComponent.ChangeHealth(-damage);
    }

    public bool IsDead()
    {
        return healthComponent.IsDead();
    }
}
