using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalTargetHealthManager : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public TutorialStageTwo tutorialStageTwo;

    public int targetHealth = 30;
    public bool targetDestroyed = false;
    public bool showNext = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        targetHealth = 30;
    }


    void Update()
    {
        if (tutorialStageTwo.showCriticalTargets)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
        }

        if (targetHealth <= 20) showNext = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) TakeDamage(1);
       
        else if (collision.CompareTag("BulletTwo")) TakeDamage(3);
    }

    private void TakeDamage(int damage)
    {
        targetHealth -= damage;
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
