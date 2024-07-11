using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public Boss1 boss1main;
    public ControlBarrier controlbarrier;
    public controlboss1sweepingarm sweeparm;
    public Animator animator;
    public Boss1PlayerStateMachine playerStateMachine;
    public GameObject player;
    public GameObject bosssoundone;
    public GameObject bosssoundtwo;
    public ClearStage clear;
    public FinishStage nclear;

    public AudioSource deadaudio;
    public AudioSource boss1bgm1;
    public AudioSource boss1bgm2;
    //public AudioClip bgmOne;
    //public AudioClip bgmTwo;
    //public AudioSource backaudio1;
    //public AudioSOurce backaudio2;

    public static int maxHP = 250;
    private float nowHP = maxHP;

    void Start()
    {
        boss1main = GetComponent<Boss1>();
        controlbarrier = GetComponent<ControlBarrier>();
        sweeparm = GetComponent<controlboss1sweepingarm>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<Boss1PlayerStateMachine>();
        clear = GetComponent<ClearStage>();
        nclear = GetComponent<FinishStage>();
        deadaudio = GetComponent<AudioSource>();
        bosssoundone = GameObject.Find("Boss1bgm1");
        bosssoundtwo = GameObject.Find("Boss1bgm2");
        boss1bgm1 = bosssoundone.GetComponent<AudioSource>();
        boss1bgm2 = bosssoundtwo.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClear()) 
        {}
        else 
        {
            if (playerStateMachine.IsGameOver())
            {
                boss1main.enabled = false;
                animator.SetBool("AttackPlayer", true);
            }
            else if (playerStateMachine.IsGameOver2())
            {
                boss1main.enabled = false;
                animator.SetBool("AttackPlayer", true);
                nclear.enabled = true;
            }
        }
        
    }
    public bool IsClear()
    {
        if (nowHP <= 0) return true;
        else return false;
    }
    public void GetDamage(int DamageNum)
    {
        nowHP-=DamageNum;
        //Debug.Log("Hurt,,");//
        if (IsClear())
        {
            boss1main.enabled = false;
            animator.SetBool("Dead", true);
            clear.enabled = true;
            GameClear();
        }
        if (nowHP <= maxHP * 40 / 100 && nowHP > 0)
        {
            animator.SetBool("Weak", true);
            boss1bgm1.enabled = false;
            boss1bgm2.enabled = true;
        }
    }

    public void GameClear()
    {
        Debug.Log("Boss Clear");
        boss1bgm1.enabled = false;
        boss1bgm2.enabled = false;
        deadaudio.Play();
        playerStateMachine.GameClear();
    }
}
