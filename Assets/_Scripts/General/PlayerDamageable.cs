using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PlayerDamageable : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;

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
