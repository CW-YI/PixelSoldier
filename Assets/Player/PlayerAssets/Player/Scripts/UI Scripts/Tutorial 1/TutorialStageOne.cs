using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialStageOne : MonoBehaviour
{
    public TMP_Text tutorialText;
    public PlayerMovement playerMovement;
    public TutorialButton tutorialButton;

    void Start()
    {
        tutorialText.fontSize = 19;
        tutorialText.text = "이동키로 움직이세요. (기본 설정: 방향키)";
        RectTransform rectTransform = tutorialText.GetComponent<RectTransform>();
    }

    [SerializeField] private float movingTime = 0f;
   
    string newText;


    [HideInInspector] public bool finishedPart1 = false;
    [HideInInspector] public bool finishedPart2 = false;
    [HideInInspector] public bool canMoveToNextStage = false;

    private float partTwoDelay = 0f;
    private float descDelay = 0f;
    private bool descDone = false;
    

    void Update()
    { 
        if (playerMovement.isMoving)
        {
            movingTime += Time.deltaTime;
            Debug.Log("moving left");
        }

        if (movingTime >= 3f && finishedPart1 == false)
        {
            newText = $"슬라이딩 버튼을 눌러 슬라이딩이 가능합니다. \n슬라이딩 도중에는 이동 속도와 점프력이 증가합니다.\n(기본 설정: 왼쪽 Shift) {playerMovement.slidingCounter} / 5";
            RectTransform rectTransform = tutorialText.GetComponent<RectTransform>();

            tutorialText.text = newText;         

            finishedPart1 = true; 
        }

        if (finishedPart1 == true && finishedPart2 == false)
        {
            newText = $"슬라이딩 버튼을 눌러 슬라이딩이 가능합니다. \n슬라이딩 도중에는 이동 속도와 점프력이 증가합니다.\n(기본 설정: 왼쪽 Shift) {playerMovement.slidingCounter} / 5";
            tutorialText.text = newText;
            partTwoDelay += Time.deltaTime;

            if (playerMovement.slidingCounter >= 5)
            {
                newText = "슬라이딩 도중에는 잠깐의 무적시간이 존재하고,\n한번 슬라이딩 한 이후에는 약간의 딜레이\n이후 다시 슬라이딩이 가능합니다.";
                tutorialText.text = newText;
                descDelay += Time.deltaTime;

                if (descDelay > 6f) descDone = true;
            }
        }

       if (descDone)
        {
            newText = "슬라이딩 점프를 이용해서 레버를\n눌러 다음 화면으로 이동하세요.";
            tutorialText.text = newText;
            finishedPart2 = true;
            canMoveToNextStage = true;
        }
    } 

}
