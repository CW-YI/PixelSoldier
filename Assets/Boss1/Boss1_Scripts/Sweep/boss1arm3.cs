using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1arm3 : MonoBehaviour
{
    

    
    public float moveSpeed = 5f;
    public float Destroytime = 15f;

    public Boss1PlayerStateMachine playerStateMachine;
    public GameObject player;

    public int armDamage = 10;
    private int cnt = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<Boss1PlayerStateMachine>();
        //Destroy(gameObject, Destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt == 0)
        {
            Sweep();
        }
        else
        {
            Sweep2();
        }
    }
    
    void Sweep()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(6, 3, 0), moveSpeed * Time.deltaTime);
        Destroy(gameObject, Destroytime);
        cnt++;
    }

    void Sweep2()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(6, 3, 0), moveSpeed * Time.deltaTime);
        Destroy(gameObject, Destroytime - 2f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") playerStateMachine.GetDamage(armDamage);
        //Debug.Log("PlayerHurt,");//
    }
}
