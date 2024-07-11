using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalTargetHealthManager : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public TutorialStageTwo tutorialStageTwo;

    public int targetHealth = 5;
    public bool targetDestroyed = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        targetHealth = 5;
    }
    void Update()
    {
        if (tutorialStageTwo.setHealth)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Target Hit");
            TakeDamage(1);
        }
        else if (collision.CompareTag("BulletTwo"))
        {
            TakeDamage(3);
        }
    }
  
    private void TakeDamage(int damage)
    {
        Debug.Log($"Damage taken {damage}");
        targetHealth -= damage;
        Debug.Log($"Current health = {targetHealth}");
        if (targetHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        targetDestroyed = true;
        TutorialStageTwo targetManager = FindObjectOfType<TutorialStageTwo>();
        if (targetManager != null)
        {
            targetManager.TargetDestroyed();
        }
        Destroy(gameObject);
    }
}
