using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lastlaserTime = Time.time;
        lastsweepTime = Time.time;
        lastPushTime = Time.time;
    }
    public Animator animator;

    
    public GameObject weapon;
    public GameObject barrier;
    public GameObject sweeparm;

    public float pushInterval = 5f;
    private float lastPushTime = 0f;

    public float laserInterval = 30f;
    private float lastlaserTime = 0f;

    public float sweepInterval = 15f;
    private float lastsweepTime = 0f;


    // Update is called once per frame
    void Update()
    {
        //if (Time.time < 7f )
        //{
        //    Push();
        //}
        //else
        //{
        //    Push2();
        //}
        //pushing pattern
        if (Time.time - lastlaserTime > laserInterval)
        {
            //start laser pattern
            animator.SetBool("AttackPlayer", true);
            Instantiate(barrier);
            lastlaserTime = Time.time;
            lastsweepTime = Time.time;
            lastPushTime = Time.time;
        }
        else if (Time.time - lastsweepTime > sweepInterval)
        {
            //start sweep pattern
            Instantiate(sweeparm, new Vector3(0, 8, 0), sweeparm.transform.rotation);
            lastsweepTime = Time.time;
            lastPushTime = Time.time;
        }
        else if (Time.time < 7f)
        {
            //start push pattern
            Push();
        }
        else
        {
            //start push pattern
            Push2();
        }
        //start laser pattern
        //if (Time.time - lastsweepTime > sweepInterval)
        //{
        //    Instantiate(sweeparm, new Vector3(0, 8, 0), sweeparm.transform.rotation);
        //    lastsweepTime = Time.time;
        //}
        //start sweep pattern
    }
    void Push()
    {
        float newX = Random.Range(-6, -2);
        float newX2 = Random.Range(3, 6);
        if (Time.time - lastPushTime > pushInterval)
        {
            Instantiate(weapon, new Vector3(newX,2.5f,0), weapon.transform.rotation);
            Instantiate(weapon, new Vector3(newX2,2.5f,0), weapon.transform.rotation);
            lastPushTime = Time.time;
        }
    }
    void Push2()
    {
        float newX = Random.Range(-6, -2);
        float newX2 = Random.Range(3, 6);
        if (Time.time - lastPushTime > pushInterval)
        {
            Instantiate(weapon, new Vector3(newX,2.5f,0), weapon.transform.rotation);
            Instantiate(weapon, new Vector3(newX2,2.5f,0), weapon.transform.rotation);
            lastPushTime = Time.time;
        }
    }
    
}
