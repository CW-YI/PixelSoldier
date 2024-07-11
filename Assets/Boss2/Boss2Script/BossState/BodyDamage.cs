using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDamage : MonoBehaviour
{
    public int damageGun1 = 1;
    public int damageGun2 = 3;
    public BossStateMachine stateMachine;

    
    void Awake()
    {
        stateMachine = GameObject.Find("enemy").GetComponent<BossStateMachine>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("boss get Damage " + damage);
            stateMachine.GetDamage(damageGun1);
        }
        else if (collision.gameObject.tag == "BulletTwo")
        {
            //Debug.Log("boss get Damage " + damage);
            stateMachine.GetDamage(damageGun2);
        }
    }
}
