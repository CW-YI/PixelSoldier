using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;

    public float moveSpeed = 5f;
    private Transform playerTransform;
    private Rigidbody2D rigid;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0) Die();
    }

    void Die ()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        rigid.bodyType = RigidbodyType2D.Dynamic; // Set the Rigidbody to Dynamic mode
        //rigid.gravityScale = 0f; // Disable gravity for the enemy
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector2 direction = playerTransform.position - transform.position;
            direction.Normalize();

            rigid.velocity = direction * moveSpeed;
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
