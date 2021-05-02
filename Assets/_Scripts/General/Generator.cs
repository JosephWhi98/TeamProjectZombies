using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Generator : MonoBehaviour, IDamageable 
{
    public float storedAmmo; 
    public float nextAmmoCycleTime;
    public int pointsForFixing; 



    public HealthComponent healthComponent; 
    public bool Broken {  get { return IsDead();  } }

    public GameObject[] gaugeBar; 

    public void Start()
    {
        healthComponent = GetComponent<HealthComponent>(); 
    }

    public void Update()
    {
        float health = healthComponent.Health;

        gaugeBar[0].SetActive(health > 0);
        gaugeBar[1].SetActive(health > 1);
        gaugeBar[2].SetActive(health > 2);
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
