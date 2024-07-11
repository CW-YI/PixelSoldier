using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1ToTwoTransition : MonoBehaviour
{
    // Name of the next scene you want to load
    public string nextSceneName;
    public GameObject cut1;
    public GameObject cut2;
    public GameObject cut3;
    public GameObject cut4;
    public FadeController fadeController;

    void Start()
    {
        // Invoke the LoadScene method after waiting for 6 seconds
        //Invoke("LoadScene", 6f);
        cut1.SetActive(true);
        StartCoroutine(ShowCuts());
    }

    IEnumerator ShowCuts()
    {
        yield return new WaitForSeconds(1.5f);
        cut2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        cut3.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        cut4.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeController.StartCoroutine(fadeController.FadeIn());
    }
    
    void LoadScene()
    {
        // Load the next scene by name
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}