using UnityEngine;

[RequireComponent(typeof(AIManager))]
[RequireComponent(typeof(HealthComponent))]
public class Zombie : MonoBehaviour, IDamageable
{
    private AIManager aIManager;
    private HealthComponent healthComponent;
    private bool hasTriggeredDeath; 

    public void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        aIManager = GameObject.FindObjectOfType<AIManager>();
    }

    public void Update()
    {
        if (IsDead() && !hasTriggeredDeath)
        {
            Die();
            hasTriggeredDeath = true; 
        }
    }

    public void TakeDamage(int amntDamage)
    {
        if (!hasTriggeredDeath)
        {
            healthComponent.ChangeHealth(-amntDamage);
        }
    }


    public bool IsDead()
    {
        return healthComponent.IsDead();
    }

    void Die()
    {
           
        aIManager.enemiesKilled++;
        Destroy(gameObject);
    }
}
