using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1sweepingsidearm : MonoBehaviour
{
    // Start is called before the first frame update

    public Boss1PlayerStateMachine playerStateMachine;
    public GameObject player;

    public int sweepingDamage = 10;

    void Start()
    {
        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<Boss1PlayerStateMachine>();
    }
    int cnt = 0;
    int num;
    [SerializeField]
    private float moveSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * Time.deltaTime);
        Sweep();
    }
    void Sweep()
    {
        //int cnt = 0;
        if (cnt==0)
        {
            num = Random.Range(0, 2);
            cnt++;
        }

        if(num==0)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            if (transform.position.x > 20)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            if (transform.position.x < -20)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") playerStateMachine.GetDamage(sweepingDamage);
        //Debug.Log("PlayerHurt,");//
    }
}
