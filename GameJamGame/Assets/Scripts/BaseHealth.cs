using UnityEngine;
using UnityEngine.Events;

public class BaseHealth : MonoBehaviour
{
    [Header("Base Health Settings")]
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int currentHealth;

    [Header("Events")]
    public static UnityEvent<int, int> onHealthChanged = new UnityEvent<int, int>();
    public static UnityEvent onBaseDestroyed = new UnityEvent();

    private bool isDestroyed = false;

    private void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isDestroyed) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        onHealthChanged.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            BaseDestroyed();
        }
    }

    private void BaseDestroyed()
    {
        Debug.Log("Game Over - Base Destroyed.");
        onBaseDestroyed.Invoke();
    }

  
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}
