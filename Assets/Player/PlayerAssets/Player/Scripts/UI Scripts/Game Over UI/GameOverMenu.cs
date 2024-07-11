using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour 
{
    public void RestartGame()
    {
        SceneManager.LoadScene(3);
    }
    public void RestartGame2()
    {
        SceneManager.LoadScene(5);
    }
    public void GoToMainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
