using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporalTimer : MonoBehaviour
{
    public TimeCheck boss1Time;
    float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = boss1Time.boss1ClearTime;   
    }

    // Update is called once per frame
    void Update()
    {
        boss1Time.totalClearTime = gameTime;
    }
}
