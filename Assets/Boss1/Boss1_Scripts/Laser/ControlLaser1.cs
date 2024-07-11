using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLaser1 : MonoBehaviour
{
    void Start()
    {
        lastlaserTime = Time.time;
    }
    [SerializeField]
    private GameObject weapon;
    //private position LaserPos1;

    [SerializeField]
    private float laserInterval = 5f;
    private float lastlaserTime = 0f;
    //private transform LaserPos1;


    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        if (Time.time - lastlaserTime > laserInterval)
        {
            Instantiate(weapon, weapon.transform.position, Quaternion.identity);
            lastlaserTime = Time.time;
        }
    }
}
