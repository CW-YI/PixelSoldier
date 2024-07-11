using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStage : MonoBehaviour
{
    public Camera mainCamera;
    public Boss1HUDScript tihud;
    //public HUDScript tehud;
    public GameObject timehud;
    public GameObject Fade;
    public Boss1FadeController fadeController;
    //public GameObject texthud;

    void Start()
    {
        timehud = GameObject.Find("Timer");
        //texthud = GameObject.Find("textHP");
        tihud = timehud.GetComponent<Boss1HUDScript>();
        //tehud = texthud.GetComponent<HUDScript>();
        StartCoroutine(ZoomInCoroutine());
        tihud.enabled = false;
        Fade = GameObject.Find("Fade");
        fadeController = Fade.GetComponent<Boss1FadeController>();
        //tehud.enabled = false;
        //Debug.Log("Zooooom");
    }

    private IEnumerator ZoomInCoroutine()
    {
        Vector3 targetPosion = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        float t = 0.0f;
        float zoomInDuration = 1.0f;
        float zoomSpeed = 0.3f;

        //float distanceToBoss = Vector3.Distance(mainCamera.transform.position, transform.position);

        while (t < zoomInDuration)
        {
            t += Time.deltaTime / zoomInDuration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosion, Time.deltaTime * zoomSpeed);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 0.6f, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        fadeController.StartCoroutine(fadeController.GameOverFade());

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(7);

    }
}
