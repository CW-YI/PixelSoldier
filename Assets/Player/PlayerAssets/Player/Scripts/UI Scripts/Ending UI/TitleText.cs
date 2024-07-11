using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    public float speed = 50f;
    public float moveUpTimer = 0;
    public float moveUpMax = 10f;

    private RectTransform rectTransform;

    public EndingUIController endingController;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endingController.finishedPart1)
        {
            Vector2 newPosition = rectTransform.anchoredPosition + Vector2.up * speed * Time.deltaTime;
            rectTransform.anchoredPosition = newPosition;
            moveUpTimer += Time.deltaTime;

            if (moveUpTimer >= moveUpMax) endingController.finishedPart1 = true;
            
        }
    }
}
