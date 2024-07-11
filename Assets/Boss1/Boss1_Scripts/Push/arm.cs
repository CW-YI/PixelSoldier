using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm: MonoBehaviour
{

    private Rigidbody2D rb2d;
    public Animator animator;
    public SpriteRenderer rend;
    public Boss1PlayerStateMachine playerStateMachine;
    public GameObject player;
    public PlayerMovementForBossBattle stunplayer;
    public AudioSource pushaudio;

    public int PushDamage = 10;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rb2d.bodyType = RigidbodyType2D.Kinematic; // Rigidbody 2D를 비활성화합니다.
        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<Boss1PlayerStateMachine>();
        pushaudio = GetComponent<AudioSource>();
        stunplayer = player.GetComponent<PlayerMovementForBossBattle>();

        Invoke("ActivateRigidbody", 1f); // 1초 후에 Rigidbody 2D를 활성화합니다.
        Invoke("DestroyGameObject", 3f); // 3초 후에 GameObject를 파괴합니다.
        // 현재 오브젝트의 x 좌표를 가져옵니다.
        float xPosition = transform.position.x;

        // x 좌표가 0보다 작으면 x 축을 뒤집습니다.
        if (xPosition < 0)
        {
            // 오브젝트의 스케일을 반전시켜 x 축을 뒤집습니다.
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Ground", true);
            pushaudio.Play();
        }
    }

    void Flip()
    {
        if (gameObject.transform.position.x < 0)
        {
            rend.flipX = true;
            //Debug.Log("Flip!");
        }
        else
        {
            rend.flipX = false;
            //Debug.Log("Flip!");
        }
    }

    private void ActivateRigidbody()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic; // 1초 후에 Rigidbody 2D를 활성화합니다.
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject); // 3초 후에 GameObject를 파괴합니다.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            playerStateMachine.GetDamage(PushDamage);
            stunplayer.StartCoroutine(stunplayer.PlayerStun());
        }
        //Debug.Log("PlayerHurt,");//
    }

    private void Update()
    {
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        // Check if the colliding object has the "Player" or "Ground" tag
        //if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        //{
            // Disable the IsTrigger property of the Collider2D component
            //GetComponent<Collider2D>().isTrigger = false;
        //}
    //}
}
