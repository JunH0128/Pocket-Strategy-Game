using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movSpeed = 2f;

    [Header("Base Damage Settings")]
    [SerializeField] private int damageToBase = 1;

    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.up * movSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && collision.collider.CompareTag("Enemy"))
        {
            ReachedBase();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            ReachedBase();
        }
    }

    private void ReachedBase()
    {
        BaseHealth baseHealth = FindObjectOfType<BaseHealth>();
        if (baseHealth != null)
        {
            baseHealth.TakeDamage(damageToBase);
        }

        EnemySpawner.onEnemyDestroy.Invoke();

        Destroy(gameObject);
    }
}
