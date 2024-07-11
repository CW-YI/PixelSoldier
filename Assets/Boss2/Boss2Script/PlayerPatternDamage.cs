using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPatternDamage : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine;
    public PlayerMovementForBossBattle playerMovement;
    public GameObject poisonEffect;
    public GameObject webEffect;
    
    //public ParticleSystem poisonEffect;
    //public GameObject poisonEffectPrefab;
    public int poisonHitDamage = 5;
    public int poisonDamage = 6;
    public int webDamage = 10;
    public int SmokeDamage = 33;

    private Coroutine poisonCoroutine;
    private Coroutine webCoroutine;
    int damageTime = 5;
    private float elapsed = 0f;

    bool invincibleState = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Poison" && !invincibleState)
        {
            playerStateMachine.GetDamage(poisonHitDamage);
            //Instantiate(poisonEffectPrefab, transform.position, Quaternion.identity);

            if (poisonCoroutine != null)
            {
                StopCoroutine(poisonCoroutine);
            }
            poisonCoroutine = StartCoroutine(PoisonDamage());
        }
        else if (collision.gameObject.tag == "Web" && !invincibleState)
        {
            playerStateMachine.GetDamage(webDamage);
            if (webCoroutine != null)
            {
                StopCoroutine(webCoroutine);
            }
            webCoroutine = StartCoroutine(WebTrapped());

        }
        else if (collision.gameObject.tag == "Smoke" && !invincibleState)
        {
            //Debug.Log("smoke");
            playerStateMachine.GetDamage(SmokeDamage);
            //playerStateMachine.StartCoroutine(playerStateMachine.InvincibleState());
        }
    }

    void Update()
    {
        if (transform.tag == "Invincible Player")
        {
            invincibleState = true;
        }
        else
        {
            invincibleState = false;
        }
    }

    IEnumerator PoisonDamage()
    {
        elapsed = 0f;
        poisonEffect.SetActive(true);
        while (elapsed < damageTime)
        {
            playerStateMachine.GetDamage(poisonDamage);
            yield return new WaitForSeconds(1f);

            //Debug.Log(elapsed +  " " + playerStateMachine.nowHP);
            elapsed += 1f;
        }
        poisonEffect.SetActive(false);
        yield return null;
    }

    IEnumerator WebTrapped()
    {
        playerMovement.is_web = true;
        webEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        webEffect.SetActive(false);
        playerMovement.is_web = false;
        yield return null;
    }
    void WebDamage()
    {

    }

}