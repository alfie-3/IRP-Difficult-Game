using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeSpeedRun;

    public void ShowWinScreen()
    {
        GetComponent<Canvas>().enabled = true;

        TimeSpan span = SpeedRunTimer.GetTimeSpan();
        timeSpeedRun.text = $"Time - {span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}:{span.Milliseconds:00}";
    }
}
