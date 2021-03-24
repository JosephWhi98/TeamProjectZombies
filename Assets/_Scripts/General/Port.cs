using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;

    public bool Open { get { return IsDead(); } }

    public void Start()
    {
        SetHealth(3);
    }

    public void Update()
    {
        health = Mathf.Clamp(health, 0, 3);
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage; 
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
