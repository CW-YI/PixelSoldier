using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{
    public enum InfoType { HP, HPText, Time }
    public InfoType type;

    public PlayerStateMachine playerStateMachine;
    public TimeCheck boss1Time;
    //public BossStateMachine bossStateMachine;
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
        gameTime = boss1Time.boss1ClearTime;
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
        boss1Time.totalClearTime = gameTime;
    }
    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.HP: //슬라이더에 적용할 값 : 현재HP/최대HP
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
