using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FadeController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject pannel;
    public GameObject canvas;
    private void Start()
    {
        spriteRenderer = pannel.GetComponent<SpriteRenderer>();
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            StartCoroutine(FadeOut());
        }
        
    }

    private void Update()
    {
        
    }

    public IEnumerator FadeIn()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new UnityEngine.Color(0, 0, 0, fadeCount);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator GameOverFade()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
            
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new UnityEngine.Color(0, 0, 0, fadeCount);
        }
        SceneManager.LoadScene(8);
    }
    public IEnumerator FadeOut()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
            
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new UnityEngine.Color(0, 0, 0, fadeCount);
        }

        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }

}