using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Boss1FadeController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject pannel;
    public GameObject canvas;
    private void Start()
    {
        spriteRenderer = pannel.GetComponent<SpriteRenderer>();
        //StartCoroutine(FadeOut());
    }

    private void Update()
    {

    }

    public IEnumerator FadeIn()
    {
        canvas.SetActive(false);
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new UnityEngine.Color(0, 0, 0, fadeCount);
        }
    }

    public IEnumerator GameOverFade()
    {
        canvas.SetActive(false);
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new UnityEngine.Color(0, 0, 0, fadeCount);
        }
    }
    public IEnumerator FadeOut()
    {
        canvas.SetActive(false);
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new UnityEngine.Color(0, 0, 0, fadeCount);
        }
        canvas.SetActive(true);
    }

}
