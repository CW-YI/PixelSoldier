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
        tutorialText.text = "�̵�Ű�� �����̼���. (�⺻ ����: ����Ű)";
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
            newText = $"�����̵� ��ư�� ���� �����̵��� �����մϴ�. \n�����̵� ���߿��� �̵� �ӵ��� �������� �����մϴ�.\n(�⺻ ����: ���� Shift) {playerMovement.slidingCounter} / 5";
            RectTransform rectTransform = tutorialText.GetComponent<RectTransform>();

            tutorialText.text = newText;         

            finishedPart1 = true; 
        }

        if (finishedPart1 == true && finishedPart2 == false)
        {
            newText = $"�����̵� ��ư�� ���� �����̵��� �����մϴ�. \n�����̵� ���߿��� �̵� �ӵ��� �������� �����մϴ�.\n(�⺻ ����: ���� Shift) {playerMovement.slidingCounter} / 5";
            tutorialText.text = newText;
            partTwoDelay += Time.deltaTime;

            if (playerMovement.slidingCounter >= 5)
            {
                newText = "�����̵� ���߿��� ����� �����ð��� �����ϰ�,\n�ѹ� �����̵� �� ���Ŀ��� �ణ�� ������\n���� �ٽ� �����̵��� �����մϴ�.";
                tutorialText.text = newText;
                descDelay += Time.deltaTime;

                if (descDelay > 6f) descDone = true;
            }
        }

       if (descDone)
        {
            newText = "�����̵� ������ �̿��ؼ� ������\n���� ���� ȭ������ �̵��ϼ���.";
            tutorialText.text = newText;
            finishedPart2 = true;
            canMoveToNextStage = true;
        }
    } 

}
