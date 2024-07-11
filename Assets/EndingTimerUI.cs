using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingTimerUI : MonoBehaviour
{
    public TMP_Text text;
    public TimeCheck bossTime;
    private void Start()
    {
        int min = Mathf.FloorToInt(bossTime.totalClearTime / 60);
        int sec = Mathf.FloorToInt(bossTime.totalClearTime % 60);
        text.text = $"Clear Time: {min:D2}'{sec:D2}\"";
    }
}
