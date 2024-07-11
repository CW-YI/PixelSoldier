using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private float laserInterval = 30f;
    private float lastlaserTime = 0f;
    float time;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time - lastlaserTime > laserInterval)
        {
            animator.SetBool("AttackPlayer", true);
            Instantiate(weapon);
            lastlaserTime = time;
        }
    }
}
