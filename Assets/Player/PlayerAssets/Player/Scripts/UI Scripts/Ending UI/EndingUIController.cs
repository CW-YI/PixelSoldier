using System.Collections;
using System.Collections.Generic;
//#if UNITY_EDITOR
//using UnityEditor.ShaderKeywordFilter;
//#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUIController : MonoBehaviour
{
    public GameObject TextController;

    private Transform titleText;
    private Transform part1;
    private Transform part2;
    private Transform part3;

    private RectTransform part1Rect;
    private RectTransform part2Rect;
    private RectTransform part3Rect;

    public bool finishedPart1 = false;
    private bool finishedPart2 = false;
    private bool finishedPart3 = false;


    private float part2Timer = 0f;
    private float part2Max = 5f;
    
    private float part3Timer = 0f;
    private float part3Max = 5f;

    private float part4Timer = 0f;
    private float part4Max = 5f;

    public float speed = 10f;
    public float moveUpTimer = 0;
    public float moveUpMax = 5f;

    void Start()
    {
        titleText = TextController.transform.Find("Title Text");
        part1 = TextController.transform.Find("Part1");
        part2 = TextController.transform.Find("Part2");
        part3 = TextController.transform.Find("Part3");


        DisableAllChildrenObjects();
    }

    void Update()
    {
        if (finishedPart1)
        {
            part1.gameObject.SetActive(true);
            part2Timer += Time.deltaTime;

            if (part2Timer >= part2Max)
            {
                part1.gameObject.SetActive(false);
                finishedPart2 = true;

            }
        }
        
        if (finishedPart2)
        {
            part2.gameObject.SetActive(true);
            part3Timer += Time.deltaTime;

            if (part3Timer >= part3Max)
            {
                part2.gameObject.SetActive(false);
                finishedPart3 = true;
            }
        }

        if (finishedPart3)
        {
            part3.gameObject.SetActive(true);
            part4Timer += Time.deltaTime;
            
            if (part4Timer >= part4Max)
            {
                SceneManager.LoadScene(0);
            }
        }    
    }

    public void DisableAllChildrenObjects()
    {
        part1.gameObject.SetActive(false);
        part2.gameObject.SetActive(false);
        part3.gameObject.SetActive(false);
    }
}
