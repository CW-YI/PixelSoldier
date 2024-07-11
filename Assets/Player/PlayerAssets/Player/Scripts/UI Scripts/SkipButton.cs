using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    public void SkipButtonTrigger()
    {

        SceneManager.LoadScene(3);
    }

}
