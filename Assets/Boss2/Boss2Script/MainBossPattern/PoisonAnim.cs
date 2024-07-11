using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
