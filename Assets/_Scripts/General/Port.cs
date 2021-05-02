﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Port : MonoBehaviour, IDamageable
{
    public HealthComponent healthComponent;

    public bool Open { get { return IsDead(); } }

    public Transform entranceTarget;
    public Transform exitTarget;

    public GameObject[] planks;

    public void Start() 
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public void Update()
    {
        float health = healthComponent.Health;

        planks[0].SetActive(health > 0);
        planks[1].SetActive(health > 1);
        planks[2].SetActive(health > 2);

    }


    public void TakeDamage(int damage = 1) 
    {
         healthComponent.ChangeHealth(-damage);  
    }

    public void Repair()
    {
        healthComponent.SetHealthAbsolute(3, true);
    }

    public bool IsDead()
    {
        return healthComponent.IsDead();
    }
}
