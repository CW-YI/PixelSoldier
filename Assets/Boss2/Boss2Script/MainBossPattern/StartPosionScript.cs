using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosionScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TopBorder")
        {
            Destroy(gameObject);
        }
    }
}
