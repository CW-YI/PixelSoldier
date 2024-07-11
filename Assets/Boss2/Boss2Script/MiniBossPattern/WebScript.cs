using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebScript : MonoBehaviour
{
    //SpriteRenderer spriteRenderer;
    //public Sprite spreadWeb;

    void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //    public GameObject player;
    //    float angle;

    //    void Start()
    //    {
    //        player = GameObject.Find("walk-1");

    //    }
    //    void Update()
    //    {
    //        angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
    //        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    //    }

    //void SpreadWeb()
    //{
    //    spriteRenderer.sprite = spreadWeb;
    //}
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("플레이어");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "TopBorder")
        {
            //Debug.Log("경계");
            Destroy(gameObject);
        }
    }

   
}
