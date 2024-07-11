using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialStageTwo : MonoBehaviour
{
    private float endTutorialDelay = 0f;
    public TMP_Text tutorialText;
    public PlayerMovement playerMovement;
    public Weapon weapon;
    public WeaponSwitch weaponSwitch;
    public NormalTargetHealthManager normalTargetHealth;
    public CriticalTargetHealthManager criticalTargetHealthManager;

    public bool canSwitchWeapon = false;
    private bool finishedPartOne = false;
    public bool finishedPartTwo = false;
    private float partThreeDelay = 0f;
    public bool finishedPartThree = false;
    public bool setHealth = false;
    public bool showNormalTargets = false;
    public bool showCriticalTargets = false;

    [SerializeField]
    private int destroyedTargetCount = 0;
    private int destroyedCriticalTargetCount = 0;
    private NormalTargetHealthManager[] targetHealthManagers;
    private CriticalTargetHealthManager[] criticalTargetHealthManagers;

    public FadeController fadeController;
    bool isSceneNext = false;

    void Start()
    {
        tutorialText.fontSize = 23;
        tutorialText.text = $"사격키로 공격해보세요.\n(기본 설정: 왼쪽 Ctrl) {weapon.shootCounter} / 10";
        RectTransform rectTransform = tutorialText.GetComponent<RectTransform>();
        targetHealthManagers = FindObjectsOfType<NormalTargetHealthManager>();
    }

    private void Update()
    {
        if (finishedPartOne == false && weapon.shootCounter <= 10)
        {
            tutorialText.text = $"사격키로 공격해보세요.\n(기본 설정: 왼쪽 Ctrl) {weapon.shootCounter} / 10";
        }
        
        if(weapon.shootCounter == 10 && finishedPartTwo == false)
        {
            canSwitchWeapon = true;
            tutorialText.text = "무기 변환키를 눌러 무기를 바꿔보세요.\n(기본설정: C키)";
            finishedPartOne = true; 
        }

        if (finishedPartOne == true && finishedPartTwo == false && weaponSwitch.isUsingWeaponTwo && partThreeDelay <= 5f)
        {
            tutorialText.text = "1번 무기는 데미지가 낮으나 발사 속도가\n빠르고, 2번 무기는 데미지가 높으나\n발사 속도가 낮습니다.";
            partThreeDelay += Time.deltaTime;

            if (partThreeDelay > 5f) finishedPartTwo = true;
        }

        if (finishedPartOne == true && finishedPartTwo == true && partThreeDelay > 5f)
        {
            setHealth = true;
            showNormalTargets = true;
            tutorialText.text = "1층에 있는 표적들을 공격 해보세요.";
        }
        if (finishedPartTwo == true && destroyedTargetCount == 4 && finishedPartThree == false)
        {
            showCriticalTargets = true;
            tutorialText.text = "2층에 있는 표적들을 슬라이딩 \n점프를 한 후 공격 해보세요.";
        }

        if (criticalTargetHealthManager.showNext == true)
        {
            tutorialText.text = "방금 피격 이펙트가 다른걸 눈치 채셨나요? \n보스들에게는 약점이 존재하고,\n약점을 공격하면 색다른 피격 이펙트와\n추가 데미지를 입힐 수 있습니다."
;       }

        if (criticalTargetHealthManager.targetDestroyed)
        {
            tutorialText.fontSize = 23;
            tutorialText.text = "튜토리얼은 여기까지입니다. \n이제 보스와 싸워서 승리하세요.";
            endTutorialDelay += Time.deltaTime;


            if (endTutorialDelay >= 5f && !isSceneNext) 
            {
                isSceneNext = true;
                fadeController.StartCoroutine(fadeController.FadeIn());
            }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void TargetDestroyed()
    {
        destroyedTargetCount++;
    }
    public void CriticalTargetDestroyed()
    {
        destroyedCriticalTargetCount++;
    }
}
