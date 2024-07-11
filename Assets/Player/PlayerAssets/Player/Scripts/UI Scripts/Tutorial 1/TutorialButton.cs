using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public TutorialStageOne tutorial;
    public bool finishedPart3 = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && tutorial.finishedPart2 == true)
        {
            Debug.Log("button pressed");
            OnButtonPress();
        }
    }

    private void OnButtonPress()
    {
        Debug.Log("Button pressed");
        finishedPart3 = true; 
    }
}
