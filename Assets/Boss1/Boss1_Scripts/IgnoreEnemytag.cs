using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreEnemytag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Collider2D 컴포넌트 가져오기
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            // 만약 물체의 태그가 "enemy"라면 Is Trigger 해제
            if (gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                collider.isTrigger = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
