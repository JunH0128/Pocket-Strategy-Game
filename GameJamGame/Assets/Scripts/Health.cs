using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    public Image healthBarFill;

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

        healthBarFill.fillAmount = (float)hitPoints / maxHitPoints;

        Debug.Log("Damage: " + dmg + " | Enemy HP: " + hitPoints);


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
            
            Enemy enemy = GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Die();
            }
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
