using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm4Control : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private float Arm4Time = 8f;
    private float lastArm4Time = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Exist4();
    }
    void Exist4()
    {
         if (Time.time - lastArm4Time > Arm4Time)
        {
            Instantiate(weapon, new Vector3(4,2,0), weapon.transform.rotation);
            lastArm4Time = Time.time;
        }
    }
}
