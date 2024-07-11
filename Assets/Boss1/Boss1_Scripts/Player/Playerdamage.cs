using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdamage : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  
        //animator = GetComponent<Animator>(); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Body")
        {
            //Debug.Log("Hit!!");
            OnDamaged(collision.transform.position);
        }
        else if(collision.gameObject.tag == "WeakPoint")
        {
            //Debug.Log("Hit!!!!!!!!!!!");
            OnDamaged(collision.transform.position);
        }
        else if(collision.gameObject.tag == "enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 6;
        //spriteRenderer.color = new Color(1, 1, 1, 0.4f); //View Alpha

        //animator.SetBool("AttackPlayer", true);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc ,1) * 7, ForceMode2D.Impulse);
        Invoke("OffDamaged", 0.7f);
    }
    void OffDamaged()
    {
        gameObject.layer = 8;
        //spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
