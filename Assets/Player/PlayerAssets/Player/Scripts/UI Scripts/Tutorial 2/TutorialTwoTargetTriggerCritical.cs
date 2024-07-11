using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTwoTargetTriggerCritical : MonoBehaviour
{
    int spriteLoadIndex = 0;

    private SpriteRenderer[] targetSprite;

    public TutorialStageTwo tutorialTwo;
    void Start()
    {
        targetSprite = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer targetSpriteRenderer in targetSprite)
        {
            if (targetSpriteRenderer != GetComponent<SpriteRenderer>())
            {
                //if (spriteLoadIndex == 0) targetSpriteRenderer.color = new Color(1, 1, 1, 1);
                targetSpriteRenderer.color = new Color(1, 1, 1, 0);

                spriteLoadIndex++;
            }
        }
    }

    void Update()
    {
        if (tutorialTwo.showCriticalTargets)
        {
            foreach (SpriteRenderer targetSpriteRenderer in targetSprite)
            {
                if (targetSpriteRenderer != GetComponent<SpriteRenderer>())
                {
                    targetSpriteRenderer.color = new Color(1, 1, 1, 1);

                }
            }
        }
    }
}
