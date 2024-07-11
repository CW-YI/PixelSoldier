using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriangleTrigger : MonoBehaviour
{
    public TutorialStageOne tutorialScriptVariable;
    public SpriteRenderer spriteRenderer;

    public float blinkInterval = 0.5f;
    private bool isBlinking = false;
    private int blinkCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    /*
    void Update()
    {
        if (tutorialScriptVariable.finishedPart2 == true && tutorialScriptVariable.finishedPart3 == false)
        {
            if (!isBlinking) StartCoroutine(BlinkSprite());
        }
        else if (tutorialScriptVariable.finishedPart3 == true) StopBlinking();
    }
    */
    IEnumerator BlinkSprite()
    {
        isBlinking = true;

        for (int i = 0; i < blinkCount * 2; ++i)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = true;

        isBlinking = false;
    }

    void StopBlinking()
    {
        if (isBlinking)
        {
            StopCoroutine(BlinkSprite());
            spriteRenderer.enabled = true;
            isBlinking = false;
        }
    }
}
