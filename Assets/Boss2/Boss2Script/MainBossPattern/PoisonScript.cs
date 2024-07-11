using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    public GameObject poisonHit;
    public GameObject poisonBoom;
    public float timeToDestroy = 0.5f;

    Vector3 pos;
    //Animator animator;
    //public PlayerStateMachine playerStateMachine;
    //public int poisonDamage = 30;
    void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyPosion();
            if (poisonHit !=null)
            {
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject hit = Instantiate(poisonHit, pos, Quaternion.identity);
                Destroy(hit, timeToDestroy);
                Destroy(gameObject, timeToDestroy);
            }
            //Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Platform")
        {
            DestroyPosion();
            if (poisonBoom != null)
            {
                //Debug.Log("플레이어 독"); // 0.43f
                pos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                GameObject boom = Instantiate(poisonBoom, pos, Quaternion.identity);
                Destroy(boom, timeToDestroy);
                Destroy(gameObject, timeToDestroy);
            }
        }
        else if (collision.gameObject.tag == "Border")
        {
            //animator.SetBool("IsPoisonDelete", true);
            Destroy(gameObject);
        }
    }

    void DestroyPosion()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) Destroy(rb);

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
       
        gameObject.SetActive(false);
    }

}
