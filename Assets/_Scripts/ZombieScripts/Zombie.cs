using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Zombie : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;
    private bool hasTriggeredDeath; 

    public void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
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
        Destroy(gameObject);
    }
}
