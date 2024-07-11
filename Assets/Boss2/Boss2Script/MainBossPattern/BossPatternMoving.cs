using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternMoving : MonoBehaviour
{
    Animator animator;
    SpriteRenderer render;
    public GameObject warning;
    Vector3 target1 = new Vector3(-6.1f, 1.5f, 0);
    Vector3 target2 = new Vector3(6.1f, 1.5f, 0); //46.3
    bool moveRight = true;
    float moveSpeed = 1.5f;
    float intervalMin = 15.0f;
    float intervalMax = 20.0f;
    float interval;
    float t;
    float warningTime = 1f;
    float jumpReadyAnim = 0.5f;
    float blinkInterval = 0.2f; // °æ°íÃ¢ÀÇ ±ôºýÀÓ °£°Ý
    float blinkCount; // °æ°íÃ¢ÀÇ ±ôºýÀÓ È½¼ö

    Vector3 left = new Vector3(-4f, 4f, 4f);
    Vector3 right = new Vector3(4f, 4f, 4f);

    public bool isMovingPatternActive = true;

    public PlayerStateMachine playerStateMachine;
    public BossStateMachine bossStateMachine;
    public GameObject player;
    public PlayerMovementForBossBattle playerMovement;
    public int bodyDamage = 10;
    public int movingDamage = 50;
    private bool isPatternOn = false;

    float stayDamageTime = 0.5f;
    float lastDamageTime = 0f;

    [SerializeField] private AudioSource MovingAudio;
    void Awake()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        StartCoroutine(MoveBossRoutine());
    }

    public IEnumerator MoveBossRoutine()
    {
        while (isMovingPatternActive)
        {
            interval = Random.Range(intervalMin, intervalMax);
            //Debug.Log(interval);
            if (bossStateMachine.is_clear)
            {
                yield break;
            }

            
            yield return new WaitForSeconds(interval - warningTime - jumpReadyAnim);
            
            //if (!isMovingPatternActive)
            //{
            //    yield return new WaitUntil(() => isMovingPatternActive);
            //}
            if (bossStateMachine.is_clear)
            {
                yield break;
            }
            StartCoroutine(ShowWarning());

            yield return new WaitForSeconds(warningTime - jumpReadyAnim);
            animator.SetBool("JumpStart", true);
            
            yield return new WaitForSeconds(jumpReadyAnim);
            animator.SetBool("JumpStart", false);
            warning.SetActive(false);
            MovingAudio.Play();

            if (bossStateMachine.is_clear)
            {
                yield break;
            }
            if (moveRight) transform.localScale = right;
            else transform.localScale = left;

            isPatternOn = true;
            //render.flipX = !moveRight;
            //animator.SetBool("isJumping", true);
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;

                if (t > 1f)
                    t = 1f;

                if (moveRight) transform.position = Vector3.Lerp(target1, target2, t);
                else transform.position = Vector3.Lerp(target2, target1, t);

                yield return null;
            }

            if (bossStateMachine.is_clear)
            {
                yield break;
            }

            animator.SetBool("JumpFinish", true);
            moveRight = !moveRight;
            isPatternOn = false;

            yield return new WaitForSeconds(jumpReadyAnim);
            animator.SetBool("JumpFinish", false);
            //animator.SetBool("isJumping", false);
            yield return null;
        }
    }

    public void StartPattern()
    {
        isMovingPatternActive = true;
        StartCoroutine(MoveBossRoutine());
    }
    public void StopPattern()
    {
        isMovingPatternActive = false;
    }

    IEnumerator ShowWarning()
    {
        blinkCount = Mathf.CeilToInt(warningTime / blinkInterval);

        for (int i = 0; i < blinkCount; i++)
        {
            warning.SetActive(!warning.activeSelf);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Player" && isPatternOn)
        {
            playerStateMachine.GetDamage(movingDamage);

            int direction = playerMovement.PlayerDirection() ? 1 : -1;

            playerStateMachine.KnockBack(direction);
        }
        else if (collision.gameObject.tag == "Player" && !isPatternOn) playerStateMachine.GetDamage(bodyDamage);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject);
        //Debug.Log("Ãæµ¹ " + Time.time);
        if (collision.gameObject.tag == "Player" && Time.time - lastDamageTime > stayDamageTime)
        {
            lastDamageTime = Time.time;
            playerStateMachine.GetDamage(bodyDamage);
        }
    }
}

