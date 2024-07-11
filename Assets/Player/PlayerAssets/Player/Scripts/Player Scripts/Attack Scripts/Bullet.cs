using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public int damage = 1;
    public Rigidbody2D rb;
    public GameObject boomPrefab;
    public GameObject weakBoomPrefab;
    public float boomDuration = 0.15f;
  

    public NormalTargetHealthManager normalHealthManager;
    void Start()
    {
        rb.velocity = transform.right * speed;     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Body")
        {
            
            StartCoroutine(PlayBoomEffect());
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "WeakPoint")
        {
            
            StartCoroutine(PlayWeakBoomEffect());
            Destroy(gameObject);
        }
    }

    IEnumerator PlayBoomEffect()
    {
        GameObject boom = Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Destroy(boom, boomDuration);
        yield return null;
    }

    IEnumerator PlayWeakBoomEffect()
    {
        GameObject boom = Instantiate(weakBoomPrefab, transform.position, Quaternion.identity);
        Destroy(boom, boomDuration);
        yield return null;
    }

}