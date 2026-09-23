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

    [SerializeField] private GameObject deathParticle;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move upward
        rb.velocity = Vector2.up * movSpeed;
    }

    public void Die()
    {
        Debug.Log("ENEMY DIED");
        Instantiate(deathParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}