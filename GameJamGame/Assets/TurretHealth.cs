using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Tower HP: " + health);

        if (health <= 0)
        {
            Debug.Log("Tower Destroyed!");
            Destroy(gameObject);
        }
    }
}
