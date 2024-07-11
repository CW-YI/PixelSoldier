using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int atk; //공격력
    public float attackDelay;

    public string atkSound;

    void Start()
    {
        //queue = new Queue<string>();
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        PlayerStateMachine.GetDamage(movingDamage);
    //        int direction = plaverMovement.PlayerDirection() ? 1 : -1;
    //        playerStateMachine.KnockBack(direction);
    //    }
    //    else if (collision.gameObject.tag == "Player")
    //    {}
    //}
    
    void Update()
    {
        
    }
}
