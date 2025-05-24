using System;
using System.Diagnostics;
using UnityEngine;

public class SpeedRunTimer : MonoBehaviour
{
    public static Stopwatch stopwatch = new();

    public static Action OnTimerStarted;
    public static Action<TimeSpan> OnTimerUpdated;

    public static void StartTimer()
    {
        stopwatch.Start();

        OnTimerStarted.Invoke();
    }

    private void Update()
    {
        if (stopwatch.IsRunning)
            OnTimerUpdated.Invoke(stopwatch.Elapsed);
    }
}
