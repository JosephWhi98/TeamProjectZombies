using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
public class HealthComponent : MonoBehaviour
{
    public int startHealth;
    public int maxHealth;

    public IntEvent onHealthChanged;
    public IntEvent onHealthChangedSync;
    public UnityEvent onDied;

    public int Health { get; private set; }
    private void Start()
    {
        Health = startHealth;
    }

    public void ChangeHealth(int delta, bool sync = true)
    {
        Health += delta;
        onHealthChanged.Invoke(delta);
        if (sync)
            onHealthChangedSync.Invoke(delta);
        Debug.Log(Health);
        Health = Mathf.Clamp(Health, 0, maxHealth);

        if (Health <= 0)
            onDied.Invoke();
    }

    public void SetHealthAbsolute(int newHealth, bool sync = true)
    {
        int diff = newHealth - Health;
        ChangeHealth(diff, sync);
    }

    public bool IsDead()
    {
        return Health <= 0;
    }
}
