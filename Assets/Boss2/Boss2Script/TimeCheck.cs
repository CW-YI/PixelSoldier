using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeCheck : MonoBehaviour
{
    public float boss1ClearTime;
    public float totalClearTime;

    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss1to2;
    public Boss1HUDScript boss1HUDScript;
    public HUDScript boss2HUDScript;
    public temporalTimer temp;
    bool isBoss2Start = false;

    void Start()
    {
        boss1 = GameObject.Find("Timer");
        boss1to2 = GameObject.Find("Timer");
        boss2 = GameObject.Find("Timer");
        
        if (boss1 != null) boss1HUDScript = boss1.GetComponent<Boss1HUDScript>();
        if (boss1to2 != null) temp = boss1.GetComponent<temporalTimer>();
        if (boss2 != null) boss2HUDScript = boss2.GetComponent<HUDScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss1HUDScript != null)
        {
            //boss1ClearTime = boss1HUDScript.gameTime;
        }
        if (boss2HUDScript != null)
        {
            //totalClearTime = boss2HUDScript.gameTime;
        }
    }
}
