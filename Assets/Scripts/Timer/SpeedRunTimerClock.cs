using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedRunTimerClock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    private void Awake()
    {
        SpeedRunTimer.OnTimerUpdated += UpdateTimerText;
        SpeedRunTimer.OnTimerStarted += StartTimer;

        timerText.enabled = false;
    }

    private void StartTimer()
    {
        timerText.enabled = true;
    }

    private void UpdateTimerText(TimeSpan span)
    {
        timerText.text = $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}:{span.Milliseconds:00}";
    }
}
