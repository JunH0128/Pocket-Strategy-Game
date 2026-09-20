using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    private int maxHitPoints;

    private void Start()
    {
        maxHitPoints = hitPoints;
    }

    public void TakeDamage(int dmg)
    {
        if (isDestroyed) return;
        hitPoints -= dmg;
        hitPoints = Mathf.Max(0, hitPoints);

       /*  EnemyHealthBar enemyHealthBar = GetComponent<EnemyHealthBar>();
        if (enemyHealthBar != null)
        {
            enemyHealthBar.UpdateHealthBar();
        } */
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            EnemyManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
        
        /*  if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            EnemyManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        } */
    }

    public int GetCurrentHealth()
    {
        return hitPoints;
    }

    public int GetMaxHealth()
    {
        return maxHitPoints;
    }

}
