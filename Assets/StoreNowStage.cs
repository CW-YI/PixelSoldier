using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreNowStage : MonoBehaviour
{
    public int nowStage;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            nowStage = 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            nowStage = 2;
        }
    }
}
