using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStateMachine : MonoBehaviour
{
    public BossPatternMoving bossMovePattern;
    public BossPatternPoison bossPoisonPattern;
    public MiniBossMoving minibossMovingPattern1;
    public MiniBossShoot minibossShootPattern1;
    public MiniBossMoving minibossMovingPattern2;
    public MiniBossShoot minibossShootPattern2;
    public MiniBossMoving minibossMovingPattern3;
    public MiniBossShoot minibossShootPattern3;
    public GameObject HUD;
    public GameObject miniSpider1;
    public GameObject miniSpider2;
    public GameObject miniSpider3;
    public GameObject player;

    public float maxHP = 150;//150
    public float nowHP;

    public GameObject explosion;
    public bool is_clear = false;
    bool is_quit = false;

    public Camera mainCamera;
    public Transform bossTransform;
    //float targetOrthoSize = 5f;
    float zoomSpeed = 0.5f;
    float distanceToBoss;
    float zoomInDuration = 1.3f;

    Vector3 targetPosion;

    public FadeController fadeController;

    [SerializeField] private AudioSource BGM;
    void Awake()
    {
        nowHP = maxHP;
        bossMovePattern = GameObject.Find("enemy").GetComponent<BossPatternMoving>();
        bossPoisonPattern = GameObject.Find("enemy").GetComponent< BossPatternPoison>();

        minibossMovingPattern1 = GameObject.Find("mini_enemy A").GetComponent<MiniBossMoving>();
        minibossShootPattern1 = GameObject.Find("mini_enemy A").GetComponent<MiniBossShoot>();

        minibossMovingPattern2 = GameObject.Find("mini_enemy A (1)").GetComponent<MiniBossMoving>();
        minibossShootPattern2 = GameObject.Find("mini_enemy A (1)").GetComponent<MiniBossShoot>();

        minibossMovingPattern3 = GameObject.Find("mini_enemy A (2)").GetComponent<MiniBossMoving>();
        minibossShootPattern3 = GameObject.Find("mini_enemy A (2)").GetComponent<MiniBossShoot>();

        mainCamera = Camera.main;
        BGM.Play();
    }

    void Update()
    {
        //if (bossTransform != null)
        // {
        //     Vector3 bossPos = bossTransform.position;
        //     mainCamera.transform.position = new Vector3(bossPos.x, bossPos.y, mainCamera.transform.position.z);

        // }

        
    }

    public bool IsClear()
    {
        if (nowHP <= 0) return true;
        else return false;
    }
    public void GetDamage(int DamageNum)
    {
        nowHP-=DamageNum;
        Debug.Log(nowHP);
        if (IsClear() && !is_clear)
        {
            is_clear = true;
            GameClear();
        }
    }

    public void SetActiveFalse()
    {
        HUD.SetActive(false);
        miniSpider1.SetActive(false);
        miniSpider2.SetActive(false);
        miniSpider3.SetActive(false);
    }
    public void GameClear()
    {
        bossPoisonPattern.StopPattern();
        StopPattern();
        ExitPattern();
        SetActiveFalse();
        Debug.Log("Boss Clear");

        StartCoroutine(ShowExplosion());
        StartCoroutine(ZoomInCoroutine());
    }

    private IEnumerator ZoomInCoroutine()
    {
        //float startOrthoSize = mainCamera.orthographicSize;
        //distanceToBoss = Vector3.Distance(mainCamera.transform.position, bossTransform.position);
        //float targetOrthoSize = distanceToBoss / 2.0f; // 원하는 크기로 조절 가능

        if (bossTransform.position.x < 0)
        {
            targetPosion = new Vector3(bossTransform.position.x - 2, bossTransform.position.y + 1.1f, bossTransform.position.z);
        }
        else if (bossTransform.position.x > 0)
        {
             targetPosion = new Vector3(bossTransform.position.x + 2, bossTransform.position.y + 1.1f, bossTransform.position.z);
        }

        float t = 0.0f;
        
        while (t<zoomInDuration)
        {
            t += Time.deltaTime / zoomInDuration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosion, Time.deltaTime * zoomSpeed);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, distanceToBoss / 2.0f, Time.deltaTime * zoomSpeed);
            yield return null;  
        }

        yield return new WaitForSeconds(1f);
        fadeController.StartCoroutine(fadeController.FadeIn());
        //SceneManager.LoadScene(5);
    }

    private void LateUpdate()
    {
        // 카메라의 위치와 크기를 보스의 위치로 조절
    }


    IEnumerator ShowExplosion()
    {
        float elapsedTime = 0f;
        while (elapsedTime <3f)
        {
            CreateExplosion();

            yield return new WaitForSeconds(0.3f);

            elapsedTime += 0.3f;
        }

        //UnityEditor.EditorApplication.ExitPlaymode();
    }

    void CreateExplosion()
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(transform.position.x - 0.8f, transform.position.x + 0.8f), UnityEngine.Random.Range(transform.position.y - 1.5f, transform.position.y + 1.5f), 0);
        GameObject explosionObj = Instantiate(explosion, pos, Quaternion.Euler(0f, 0f, 0f));

        Destroy(explosionObj, 1.5f);
    }
    public void ExitPattern()
    {
        bossMovePattern.StopCoroutine(bossMovePattern.MoveBossRoutine());
        bossPoisonPattern.StopCoroutine(bossPoisonPattern.PoisonPattern());

        minibossMovingPattern1.StopCoroutine(minibossMovingPattern1.MovePattern());
        minibossMovingPattern2.StopCoroutine(minibossMovingPattern2.MovePattern());
        minibossMovingPattern3.StopCoroutine(minibossMovingPattern3.MovePattern());

        minibossShootPattern1.StopCoroutine(minibossShootPattern1.ShootPattern());
        minibossShootPattern2.StopCoroutine(minibossShootPattern2.ShootPattern());
        minibossShootPattern3.StopCoroutine(minibossShootPattern3.ShootPattern());
    }
    public void StopPattern()
    {
        //Debug.Log("Posion Start");
        //Debug.Log("게임종료");
        bossMovePattern.StopPattern();
        minibossMovingPattern1.StopPattern();
        minibossShootPattern1.StopPattern();

        minibossMovingPattern2.StopPattern();
        minibossShootPattern2.StopPattern();

        minibossMovingPattern3.StopPattern();
        minibossShootPattern3.StopPattern();
    }

    public void StartPattern()
    {
        //Debug.Log("Posion Stop");
        bossMovePattern.StartPattern();
        minibossMovingPattern1.StartPattern();
        minibossShootPattern1.StartPattern();

        minibossMovingPattern2.StartPattern();
        minibossShootPattern2.StartPattern();

        minibossMovingPattern3.StartPattern();
        minibossShootPattern3.StartPattern();
    }
}
