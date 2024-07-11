using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerStateMachine : MonoBehaviour
{

    public int maxHP = 1000;
    public int nowHP;
    public float invincibleTime = 0.1f;

    Rigidbody2D rigid;
    public PlayerMovementForBossBattle playerMovement;
    Animator animator;
    public Camera mainCamera;
    public BossStateMachine bossStateMachine;
    public GameObject mainBoss;
    bool isGameOver = false;

    public FadeController fadeController;
    void Awake()
    {
        nowHP = maxHP;
        rigid = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
    }

    public bool IsGameOver()
    {
        if (nowHP <= 0)
        {
            return true;
        }

        else return false;
    }

    public void GetDamage(int DamageNum)
    {
        nowHP -= DamageNum;
        StartCoroutine(InvincibleTime());
       // Debug.Log("³²Àº HP : " + nowHP);
    }

    public IEnumerator InvincibleTime()
    {
        transform.tag = "Invincible Player";
        yield return new WaitForSeconds(0.1f);
        transform.tag = "Player";
    }
    public void KnockBack(int direction)
    {
        //Debug.Log(new Vector2(direction, 1));
        Debug.Log("knock back");
        playerMovement.StartCoroutine(playerMovement.PlayerStun());

        rigid.velocity = new Vector2(15 * direction, 10);
    }
    public void GameOver()
    {
        Debug.Log("Player Dead");
        //animator.SetBool("IsDead", true);
        if (playerMovement.PlayerStun() != null)
        {
            playerMovement.StopCoroutine(playerMovement.PlayerStun());
            //animator.SetTrigger("Dead");
            
        }
        playerMovement.is_stun = true;
        playerMovement.enabled = false;
        
        bossStateMachine.SetActiveFalse();

        mainBoss.SetActive(false);
        animator.Play("playerDeadOne");
        //animator.SetTrigger("Dead");
        //animator.SetBool("IsDead", false);
        //animator.SetBool("Deading", true);
        StartCoroutine(ZoomInCoroutine());
        //Destroy(gameObject);
    }

    private IEnumerator ZoomInCoroutine()
    {  
        Vector3 targetPosion = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        float t = 0.0f;
        float zoomInDuration = 1.3f;
        float zoomSpeed = 0.5f;
        
        //float distanceToBoss = Vector3.Distance(mainCamera.transform.position, transform.position);

        while (t < zoomInDuration)
        {
            t += Time.deltaTime / zoomInDuration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosion, Time.deltaTime * zoomSpeed);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 0.6f, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        fadeController.StartCoroutine(fadeController.GameOverFade());
        //SceneManager.LoadScene(6);
    }
    public IEnumerator InvincibleState()
    {
        yield return new WaitForSeconds(invincibleTime);
    }
    
    void Update()
    {
        if (IsGameOver() && !isGameOver)
        {
            isGameOver = true;
            //animator.SetBool("isDead", true);
            GameOver();
        }
        animator.SetInteger("playerHealth", nowHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            nowHP = 0;
        }
    }
}
