using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1WeakDamage : MonoBehaviour
{
    public static int damage = 3;
    public Boss1StateMachine stateMachine;
    void Start()
    {
        stateMachine = GameObject.Find("Boss1Main").GetComponent<Boss1StateMachine>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("boss get Damage " + damage);
            stateMachine.GetDamage(damage);
        }
    }
    // Update is called once per fram
    void Update()
    {
        
    }
}
