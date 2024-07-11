using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTwoTargetTriggerNormal : MonoBehaviour
{
    int spriteLoadIndex = 0;
    private SpriteRenderer[] targetSprite;
    private CapsuleCollider2D capsuleCollider;
    public TutorialStageTwo tutorialTwo;

    private bool isSpawnedTargets = false;
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
        if (tutorialTwo.showNormalTargets && isSpawnedTargets == false)
        {
            isSpawnedTargets = true;
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
