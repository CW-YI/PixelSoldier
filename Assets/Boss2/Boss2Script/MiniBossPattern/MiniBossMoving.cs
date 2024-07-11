using System.Collections;
using System.Collections.Generic;
//using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class MiniBossMoving : MonoBehaviour
{
    int bossID;
    public float patternIntervalMin = 8f;
    float patternIntervalMax = 10f;
    float patternInterval;
    bool patternOn = false;
    float moveSpeed = 5f;
    Vector3 startPosition;
    Vector3 platformPosition;
    public GameObject platform;
    public GameObject warning;
    float warningTime = 1f;
    float blinkInterval = 0.2f;
    float blinkCount;
    Rigidbody2D rigid;
    Animator animator;

    public PlayerMovementForBossBattle playerMovement;
    public PlayerStateMachine playerStateMachine;
    public int miniBodyDamage = 10;
    public int miniMovingDamage = 33;
    private bool isPatternOn = false;

    public GameObject smoke;
    public float smokeToDestroy = 1f;
    #region //이름
    #endregion
    public bool isMiniMovingActive = true;

    private Coroutine stunCoroutine;
    [SerializeField] private AudioSource miniMovingAudio;

    //bool upTime = false;
    //float moveTime = 0f;
        
void Awake()
    {
        //tilemap = platform.GetComponent<Tilemap>(); ;
        rigid = GetComponent<Rigidbody2D>();
        bossID = GetBossID();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        startPosition = transform.position;
        platformPosition = platform.transform.position;
        //Debug.Log(bossID + " : " +  startPosition);
        StartCoroutine(MovePattern());
    }

    void Update()
    {
        //if (upTime)
        //{
        //    moveTime += Time.deltaTime;
        //}
    }

    
    public IEnumerator MovePattern()
    {
        yield return new WaitForSeconds(bossID * 2);
        while (isMiniMovingActive)
        {
            patternInterval = Random.Range(patternIntervalMin, patternIntervalMax);

            yield return new WaitForSeconds(patternInterval - warningTime - 0.5f);

            animator.SetBool("Move", true);
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(WarningMove());
            yield return new WaitForSeconds(warningTime);
            warning.SetActive(false);
            isPatternOn=true;
            //yield return new WaitForSeconds(patternInterval);

            //if (bossIndex == bossID)
           //if (!isMiniMovingActive)
           // {
           //     yield return new WaitUntil(() => isMiniMovingActive);
           // }
            StartCoroutine(MoveRoutine());

            yield return new WaitUntil(() => !patternOn);
            isPatternOn = false;

            //patternOn = true;
            yield return new WaitForSeconds(patternInterval);
        }
    }

    IEnumerator WarningMove()
    {
        //upTime = true;
        // 0.315 ~ 0.32초 정도
        
        while (transform.position.y < startPosition.y + 1.5f)
        {
            rigid.velocity = Vector2.up * moveSpeed;
            yield return null;
        }
        rigid.velocity = Vector2.zero;
        
        //upTime = false;
        //Debug.Log(moveTime);
    }

    IEnumerator MoveRoutine()
    {
        patternOn = true;

        //Debug.Log(transform.position.y + " " + platformPosition.y + " " + startPosition.y);
        while (transform.position.y > platformPosition.y + 1f)
        {
            rigid.velocity = Vector2.down * moveSpeed * 3;
            //Debug.Log(transform.position);
            yield return null;
        }

        smoke.SetActive(true);
        miniMovingAudio.Play();
        rigid.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("Move", false);
        smoke.SetActive(false);
        patternOn = false;
        while (transform.position.y < startPosition.y)
        {
            rigid.velocity = Vector2.up * moveSpeed;
            yield return null;
        }
        
        rigid.velocity = Vector2.zero;
        
    }

    //IEnumerator ShowWarning()
    //{
    //    blinkCount = Mathf.CeilToInt(warningTime / blinkInterval);

    //    for (int i = 0; i < blinkCount; i++)
    //    {
    //        warning.SetActive(!warning.activeSelf);
    //        yield return new WaitForSeconds(blinkInterval);
    //    }
    //}

    int GetBossID()
    {
        if (transform.position.x < -2) bossID = 1;
        else if (transform.position.x > 2) bossID = 2;
        else bossID = 0;

        return bossID;
    }
    public void StartPattern()
    {
        //Debug.Log("moving " + bossID);
        isMiniMovingActive = true;
        StartCoroutine(MovePattern());
    }
    public void StopPattern()
    {
        isMiniMovingActive = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isPatternOn)
        {
            playerStateMachine.GetDamage(miniMovingDamage);
            playerMovement.StartCoroutine(playerMovement.PlayerStun());
        }
        else if (collision.gameObject.tag == "Player" && !isPatternOn) playerStateMachine.GetDamage(miniBodyDamage);
    }

}
   
