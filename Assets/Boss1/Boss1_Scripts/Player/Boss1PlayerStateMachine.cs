using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1PlayerStateMachine : MonoBehaviour
{

    public PlayerMovementForBossBattle playermove;
    public WeaponForBossBattle weaponbattle;
    //public PlayerInput playerinput;
    public Playerdamage damage;
    public FinishStage clear;
    public Rigidbody2D rigid;
    public GameObject boss1;
    public Boss1StateMachine boss1statemachine;


    public  int maxHP = 1000;
    public int nowHP;
    public float invincibleTime = 0.1f;
    private int flag;

    public Animator animator;

    bool isGameOver = false;
    bool isGameOver2 = false;
    bool deadAnimPlayed = false;
    void Awake()
    {
        nowHP = maxHP;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        playermove = GetComponent<PlayerMovementForBossBattle>();
        weaponbattle = GetComponent<WeaponForBossBattle>();
        //playerinput = GetComponent<Playerinput>();
        damage = GetComponent<Playerdamage>();
        clear = GetComponent<FinishStage>();
        boss1 = GameObject.Find("Boss1Main");
        boss1statemachine = boss1.GetComponent<Boss1StateMachine>();

        animator.SetInteger("playerHealth", nowHP);

    }

    public bool IsGameOver()
    {
        if (boss1statemachine.IsClear()) 
        {
            return false;
        }
        else 
        {
            if (nowHP <= 0) return true;
            else return false;
        }
    }

    public bool IsGameOver2()
    {
        if (boss1statemachine.IsClear())
        {
            return false;
        }
        else
        {
            if (flag == 1) return true;
            else return false;
        }
    }

    public void GetDamage(int DamageNum)
    {
        nowHP -= DamageNum;
        //Debug.Log("PlayerHurt,,");//
        if (IsGameOver())
        {
            GameOver();
            clear.enabled = true;
        }
    }
    public void GameOver()
    {
        Debug.Log("Player Dead");
        if (!deadAnimPlayed)
        {
            animator.Play("playerDeadOne");
            deadAnimPlayed = true;
        }

        animator.SetInteger("playerHealth", 0);

        playermove.enabled = false;
        weaponbattle.enabled = false;
        //playerinput.enabled = false;
        damage.enabled = false;
        //SceneManager.LoadScene(1);
        //clear.enabled = true;
        //Destroy(gameObject);
    }

    public void GameClear()
    {
        playermove.enabled = false;
        weaponbattle.enabled = false;
        //playerinput.enabled = false;
        damage.enabled = false;
        animator.SetInteger("playerHealth", 0);
    }

    //public IEnumerator InvincibleState()
    //{
    //    yield return new WaitForSeconds(invincibleTime);
    //}

    void Update()
    {
        if (IsGameOver2())
        {
            GameOver();
            //SceneManager.LoadScene(1);
        }

        //animator.SetFloat("playerHealth", nowHP);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            flag = 1;
        }
        else
        {
            flag = 0;
        }
    }

    public void KnockBack(int direction)
    {
        //Debug.Log(new Vector2(direction, 1));
        Debug.Log("knock back");
        playermove.StartCoroutine(playermove.PlayerStun());

        rigid.velocity = new Vector2(10 * direction, 10);
    }
}
