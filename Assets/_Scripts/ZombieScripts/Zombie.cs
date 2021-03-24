using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float health = 50f;
    
    public void TakeDamage(float amntDamage)
    {
        health -= amntDamage;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
