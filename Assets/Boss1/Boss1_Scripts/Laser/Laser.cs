using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    public float DestroyTime = 1f;
    public Boss1PlayerStateMachine playerStateMachine;
    public GameObject player;
    
    public int laserDamage = 20;

    void Start()
    {
        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<Boss1PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, DestroyTime);
    }

   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") playerStateMachine.GetDamage(laserDamage);
        //Debug.Log("PlayerHurt,");//
    }
}
