using System.Collections;
using UnityEngine;

public class BossPatternPoison : MonoBehaviour
{
    float patternIntervalMin = 20f;
    float patternIntervalMax = 30f;
    float patternInterval;
    float durationPattern = 1.8f;//1.8
    float warningTime = 5f;
    float poisonStartTime = 2f;
    float t = 0f;
    float zRotation = 49f;
    float speed = 5f;
    Vector3 target = new Vector3(0.05f, 5.7f, 0);

    public BossStateMachine stateMachine;
    
    [SerializeField]
    public GameObject poison;
    [SerializeField]
    public GameObject startPoisonObject;

    Rigidbody2D rigidPoision;
    Animator animator;
    public bool isPoisonPatternActive = false;
    bool patternStart = true;

    [SerializeField] private AudioSource poisonAudio;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        stateMachine = GameObject.Find("enemy").GetComponent<BossStateMachine>();
        //PoisonWarning();
        //isPoisonPatternActive = true;
        StartCoroutine(PoisonPattern());
    }

    void Update()
    {
        //Debug.Log(stateMachine.nowHP / stateMachine.maxHP);
        if (stateMachine.nowHP / stateMachine.maxHP <= 0.4f && patternStart)
        {
            Debug.Log("poison pattern start");
            patternStart = false;
            isPoisonPatternActive = true;
            StartCoroutine(PoisonPattern());
        }
    }
    public IEnumerator PoisonPattern()
    {
        while (isPoisonPatternActive)
        {
            //Debug.Log("test");
            if (stateMachine.is_clear)
            {
                yield break;
            }
            patternInterval = Random.Range(patternIntervalMin, patternIntervalMax);
            
            yield return new WaitForSeconds(patternInterval - warningTime * 2);
            stateMachine.StopPattern();
            

            if(stateMachine.is_clear)
            {
                yield break;
            }

            yield return new WaitForSeconds(warningTime);
            //PoisonWarning();
            

            if (stateMachine.is_clear)
            {
                yield break;
            }

            
            yield return new WaitForSeconds(warningTime - poisonStartTime);
            animator.SetBool("Poison", true);
            poisonAudio.Play();
            yield return new WaitForSeconds(poisonStartTime);

            if (stateMachine.is_clear)
            {
                yield break;
            }

            animator.SetBool("Poison", false);
            StartCoroutine(PoisonRoutine());

            yield return new WaitForSeconds(durationPattern + 1.5f);
            if (stateMachine.is_clear)
            {
                yield break;
            }
            
            stateMachine.StartPattern();
        }
        
    }

    IEnumerator PoisonRoutine()
    {
        float poisonInterval = 0.03f;
        while (t < durationPattern)
        {
            //Debug.Log(t);
            t += Time.deltaTime;

            if (t>=poisonInterval)
            {
                CreatePosion();
                poisonInterval += 0.03f;
            }

            yield return null;
        }
        t = 0f;
        yield return null;
    }

    void  PoisonWarning()
    {
        if (transform.position.x > 0)
        {
            zRotation = 139f;
        }
        else
        {
            zRotation = 49f;
        }

        //GameObject bigPoison = Instantiate(startPoison, transform.position, Quaternion.Euler(0f, 0f, zRotation));
        //Rigidbody2D rigidPoison = bigPoison.GetComponent<Rigidbody2D>();
        //Vector2 direction = transform.forward;
        //rigidPoison.AddForce(direction * 30f, ForceMode2D.Impulse);

        GameObject startPoison = Instantiate(startPoisonObject, transform.position, Quaternion.identity);
        Rigidbody2D rigidPoison = startPoison.GetComponent<Rigidbody2D>();

        Vector2 direction = (target - transform.position).normalized;
        startPoison.transform.rotation = Quaternion.Euler(0, 0, zRotation);
        rigidPoison.AddForce(direction * speed, ForceMode2D.Impulse);

    }

    void CreatePosion()
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-5.3f, 5.3f), 5.5f, 0);
        Instantiate(poison, pos, Quaternion.Euler(0f, 0f, 0f));
        //poison.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
    }

    public void StartPattern()
    {
        isPoisonPatternActive = true;
    }
    public void StopPattern()
    {
        isPoisonPatternActive = false;
    }
}
