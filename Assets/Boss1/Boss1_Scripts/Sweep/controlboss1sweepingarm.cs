using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlboss1sweepingarm : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    //public GameObject subweapon;
    [SerializeField]
    private float sweepInterval = 15f;
    private float lastsweepTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time - lastsweepTime > sweepInterval - 2f)
        //{
            //Instantiate(subweapon);
        //}
        if (Time.time - lastsweepTime > sweepInterval)
        {
            Instantiate(weapon, new Vector3(0, 8, 0), weapon.transform.rotation);
            lastsweepTime = Time.time;
        }
        //Destroy(gameObject, 3f);
        //Instantiate(weapon);
    }
}
