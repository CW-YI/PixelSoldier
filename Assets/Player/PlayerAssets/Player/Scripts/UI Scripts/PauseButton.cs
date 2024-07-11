using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GoToSettings()
    {

    }
}

