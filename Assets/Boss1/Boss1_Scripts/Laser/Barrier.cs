using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float transparencySpeed = 1.0f; // 투명도 증가 속도
    public Color targetColor = Color.red; // 최종 색상
    public float gobackSpeed = 1.0f; // 본래 색상으로 돌아가려는 속도
    public float Destroytime = 5f;

    public Boss1PlayerStateMachine playerStateMachine;
    public GameObject player;

    public int barrierDamage = 5;
    private bool canDealDamage = true;


    private Material objectMaterial; // 오브젝트의 Material
    private Color originalColor; // 초기 색상
    private float currentTransparency; // 현재 투명도 (0~1)

    private bool increasingTransparency = true; // 투명도를 증가시키는지 여부

    private void Start()
    {
        // 오브젝트의 Renderer 컴포넌트로부터 Material을 가져옴
        Renderer renderer = GetComponent<Renderer>();
        objectMaterial = renderer.material;

        // 오브젝트의 초기 색상과 투명도를 저장
        originalColor = objectMaterial.color;
        currentTransparency = 0.0f;

        // Start the coroutine for transparency change
        StartCoroutine(StartingChange());

        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<Boss1PlayerStateMachine>();

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") playerStateMachine.GetDamage(barrierDamage);
        //Debug.Log("PlayerHurt,");//
    }

    void OnColliderStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canDealDamage)
            {
                playerStateMachine.GetDamage(barrierDamage);
                canDealDamage = false;
                StartCoroutine(ResetDamageInterval());
            }
        }
        //Debug.Log("PlayerHurt,");//
    }

    private IEnumerator ResetDamageInterval()
    {
        yield return new WaitForSeconds(0.2f);
        canDealDamage = true;
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        playerStateMachine.GetDamage(barrierDamage);
    //    }
    //}

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        playerStateMachine.GetDamage(barrierDamage);
    //    }
    //}



    private System.Collections.IEnumerator StartingChange()
    {
        while (true)
        {
            // 투명도를 증가시키거나 감소시킴
            if (increasingTransparency)
            {
                currentTransparency += transparencySpeed * Time.deltaTime;
                if (currentTransparency >= 1.0f)
                {
                    currentTransparency = 1.0f;
                    increasingTransparency = false;

                    // 최종 색상으로 변경
                    objectMaterial.color = targetColor;
                }
            }
            else
            {
                currentTransparency -= transparencySpeed * Time.deltaTime * gobackSpeed;
                if (currentTransparency <= 0.0f)
                {
                    currentTransparency = 0.0f;
                    increasingTransparency = true;

                    // 초기 색상으로 변경
                    objectMaterial.color = originalColor;
                }
            }

            // 투명도를 반영하여 Material의 색상을 업데이트
            Color newColor = objectMaterial.color;
            newColor.a = currentTransparency;
            objectMaterial.color = newColor;

            // Check if the color transition is complete
            if (!increasingTransparency && currentTransparency == 1.0f)
            {
                // Wait for the specified time before destroying the object
                yield return new WaitForSeconds(Destroytime);
                Destroy(gameObject);
            }

            // Yielding here ensures the coroutine will wait for the next frame before continuing the loop
            yield return null;
        }
    }
    


}