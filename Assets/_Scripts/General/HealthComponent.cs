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

        Health = Mathf.Clamp(Health, 0, maxHealth);

        Debug.Log(gameObject.name + "'s HP = " + Health);

        if (Health <= 0)
            onDied.Invoke();

    }

    public void SetHealthAbsolute(int newHealth, bool sync = true)
    {
        int diff = newHealth - Health;
        ChangeHealth(diff, sync);
    }
    [ContextMenu("LogHealth")]
    public void LogHealth()
    {
        Debug.Log(Health);
    }
    [ContextMenu("KillNetworked")]
    public void Kill()
    {
        SetHealthAbsolute(0);
    }
    public bool IsDead()
    {
        return Health <= 0;
    }
}
