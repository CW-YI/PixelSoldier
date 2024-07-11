using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmControl : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private float ArmTime = 8f;
    private float lastArmTime = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Exist();
    }
    void Exist()
    {
         if (Time.time - lastArmTime > ArmTime)
        {
            Instantiate(weapon, new Vector3(-4,2,0), weapon.transform.rotation);
            lastArmTime = Time.time;
        }
    }
}
