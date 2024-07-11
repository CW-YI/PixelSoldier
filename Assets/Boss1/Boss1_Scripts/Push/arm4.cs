using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm4 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private float minX = 20;

    //[SerializeField]
    //private float SweepTime = 20f;
    //private float lastSweepTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sweep();
    }
    void Sweep()
    {
        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        if (transform.position.x > minX)
        {
            Destroy(gameObject);
        }

    }
}
