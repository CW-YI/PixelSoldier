using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointDamage : MonoBehaviour
{
    public static int weakGun1;
    public static int weakGun2;

    public BossStateMachine stateMachine;
    public BodyDamage damage;

    void Awake()
    {
        stateMachine = GameObject.Find("enemy").GetComponent<BossStateMachine>();
        weakGun1 = damage.damageGun1 * 2;
        weakGun2 = damage.damageGun2 * 2;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("boss get Damage "+ damage);
            stateMachine.GetDamage(weakGun1);
        }
        else if (collision.gameObject.tag == "BulletTwo")
        {
            //Debug.Log("boss get Damage "+ damage);
            stateMachine.GetDamage(weakGun2);
        }
    }
}
