using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1sweepingarm : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float delayTime = 2f;
    private bool moveLeft = false;
    private bool moveRight = false;
    private float elapsedTime = 0f;
    public int sweepDamage = 40;

    public Boss1PlayerStateMachine playerStateMachine;
    public GameObject player;
    public PlayerMovementForBossBattle stunplayer;

    private Transform playerTransform;
    private Rigidbody2D rigid;


    private Rigidbody2D rb2d;
    //[SerializeField]
    public GameObject weapon;
    public GameObject weapon2;
    public GameObject arm2;
    public GameObject arm3;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //SetMoveDirection(); // 이동 방향을 랜덤하게 설정합니다.
        StartCoroutine(EnableRigidbodyAfterDelay()); // 2초 후에 Rigidbody 2D를 활성화하는 코루틴을 시작합니다.
        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<Boss1PlayerStateMachine>();
        stunplayer = player.GetComponent<PlayerMovementForBossBattle>();
        //Destroy(arm2);
        //Destroy(arm3);
    }

    private void Update()
    {
        // 지정된 시간이 지났을 때 이동을 시작합니다.
        if (moveLeft)
        {
            //Instantiate(weapon);
            //transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            if (transform.position.x < -10)
            {
                Instantiate(arm2);
                Instantiate(arm3);
                Destroy(gameObject);
            }
        }
        else if (moveRight)
        {
            //transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            //Instantiate(weapon);
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            if (transform.position.x > 10)
            {
                Instantiate(arm2);
                Instantiate(arm3);
                Destroy(gameObject);
            }
        }
    }

    private void SetMoveDirection()
    {
        // 왼쪽 혹은 오른쪽으로 이동할 방향을 랜덤하게 선택합니다.
        if (Random.Range(0, 2) == 0)
        {
            moveLeft = true;
            Instantiate(weapon);
        }
        else
        {
            moveRight = true;
            Instantiate(weapon2);
        }
    }

    private System.Collections.IEnumerator EnableRigidbodyAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); // delayTime(2초) 만큼 대기합니다.

        //rb2d.bodyType = RigidbodyType2D.Kinematic; // Rigidbody 2D를 Kinematic으로 설정하여 물리 시뮬레이션을 비활성화합니다.
        //rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        //rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        //rb2d.constraints = RigidbodyConstraints2D.FreezeRotationZ;

        SetMoveDirection(); // 2초 후에 이동 방향을 랜덤하게 설정합니다.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStateMachine.GetDamage(sweepDamage);
            stunplayer.StartCoroutine(stunplayer.PlayerStun());
            int direction = stunplayer.PlayerDirection() ? 1 : -1;

            playerStateMachine.KnockBack(direction);
            //stunplayer.is_stun = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStateMachine.GetDamage(sweepDamage);
            stunplayer.StartCoroutine(stunplayer.PlayerStun());
            //stunplayer.is_stun = true;
        }
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        rigid.bodyType = RigidbodyType2D.Dynamic; // Set the Rigidbody to Dynamic mode
        //rigid.gravityScale = 0f; // Disable gravity for the enemy
    }

    //private void FixedUpdate()
    //{
    //    if (playerTransform != null)
    //    {
    //        Vector2 direction = playerTransform.position - transform.position;
    //        direction.Normalize();
    //
    //        rigid.velocity = direction * moveSpeed;
    //    }
    //    else
    //    {
    //        rigid.velocity = Vector2.zero;
    //    }
    //}
}
