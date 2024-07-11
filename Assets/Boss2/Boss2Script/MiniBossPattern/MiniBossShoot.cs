using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBossShoot : MonoBehaviour
{
    public GameObject bulletObject;
    public GameObject player;
    //public GameObject warning;

    int bossID;
    float warningTime = 2f;
    float patternIntervalMin = 8f;
    float patternIntervalMax = 15f;
    float patternInterval;
    float speed = 0.0009f;
    float blinkInterval = 0.2f;
    float blinkCount;

    public bool isMiniShootActive = true;

    
    public Sprite spreadWeb;
    public Sprite cobWeb;
    public float webSpreadDistance = 3f;

    Animator animator;
    void Awake()
    {
        
        bossID = GetBossID();
        animator = GetComponent<Animator>();
        StartCoroutine(ShootPattern());
    }

    void Update()
    {
        //SpreadWeb();
        //Debug.Log("거미줄과 플레이어 거리 " + distanceToPlayer);
        
    }

    public IEnumerator ShootPattern()
    {
        if (player != null)
        {
            yield return new WaitForSeconds((2 - bossID) * 2);

            while (isMiniShootActive)
            {
                patternInterval = Random.Range(patternIntervalMin, patternIntervalMax);

                yield return new WaitForSeconds(patternInterval - warningTime);

                //if (!isMiniShootActive)
                //{
                //    yield return new WaitUntil(() => isMiniShootActive);
                //}

                animator.SetBool("Web", true);
                StartCoroutine(ShowWarning());
                yield return new WaitForSeconds(warningTime);
                //warning.SetActive(false);

                animator.SetBool("Web", false);
                if (player != null)
                {
                    Fire();
                }

            }
        }
    }
    
    IEnumerator ShowWarning()
    {
        blinkCount = Mathf.CeilToInt(warningTime / blinkInterval);

        for (int i = 0; i < blinkCount; i++)
        {
            //warning.SetActive(!warning.activeSelf);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
    void Fire()
    {
        GameObject bullet = Instantiate(bulletObject, transform.position, Quaternion.identity);
        Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, 90 + angle);
        //bullet.transform.LookAt(player.transform);
        rigidBullet.AddForce(direction * speed, ForceMode2D.Impulse);

    }

    //void SpreadWeb()
    //{
    //    GameObject bulletClone = GameObject.FindWithTag("Web");

    //    if (bulletClone != null)
    //    {
    //        SpriteRenderer spriteRenderer = bulletClone.GetComponent<SpriteRenderer>();
    //        float distanceToPlayer = Vector3.Distance(bulletClone.transform.position, player.transform.position);

    //        //Debug.Log("플레이어 : " + player.transform.position + " 거미줄 : " + bulletClone.transform.position);
    //        //Debug.Log(distanceToPlayer);
    //        if (distanceToPlayer <= webSpreadDistance)
    //        {
    //            //Debug.Log("거미줄 펼치기");
    //            bulletClone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //            //spriteRenderer.sprite = spreadWeb;
    //        }
    //        else
    //        {
    //            bulletClone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //            //spriteRenderer.sprite = cobWeb;
    //        }
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
        //Debug.Log("shoot " + bossID);
        isMiniShootActive = true;
        StartCoroutine(ShootPattern());
    }
    public void StopPattern()
    {
        isMiniShootActive = false;
    }
}
