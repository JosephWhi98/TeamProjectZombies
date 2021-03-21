using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [SerializeField] private int health;

    public bool Open { get { return (health <= 0); } }

    public void Update()
    {
        health = Mathf.Clamp(health, 0, 3);
    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage; 
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth; 
    }
}
