using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss1HUDScript : MonoBehaviour
{
    public enum InfoType { HP, HPText, Time }
    public InfoType type;

    public Boss1PlayerStateMachine playerStateMachine;
    public TimeCheck timecheck; 
    Text myText;
    Slider mySlider;

    float maxHP;
    float nowHP;

    public float gameTime;
    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();

        maxHP = playerStateMachine.maxHP;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        //Stop();
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        gameTime += Time.deltaTime;
        timecheck.boss1ClearTime = gameTime;
    }
    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.HP: //�����̴��� ������ �� : ����HP/�ִ�HP
                nowHP = playerStateMachine.nowHP;
                mySlider.value = nowHP / maxHP;
                break;

            case InfoType.HPText:
                nowHP = playerStateMachine.nowHP;
                myText.text = string.Format("{0:F0}/{1:F0}", nowHP, maxHP);
                break;

            case InfoType.Time:
                int min = Mathf.FloorToInt(gameTime / 60);
                int sec = Mathf.FloorToInt(gameTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
        }
    }
}
