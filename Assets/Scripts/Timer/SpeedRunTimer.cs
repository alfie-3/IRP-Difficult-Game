using System;
using UnityEngine;

public class SpeedRunTimer : MonoBehaviour
{
    public static double time;
    public static TimeSpan timerTimespan => TimeSpan.FromSeconds(time);

    public static bool IsRunning;

    public static Action OnTimerStarted;
    public static Action<TimeSpan> OnTimerUpdated;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        time = 0;
        IsRunning = false;
    }

    private void Start()
    {
        double savedTime = TryGetTimeFromPrefs();

        if (savedTime != 0)
        {
            time = savedTime;
            StartTimer();
        }
    }

    public static void StartTimer()
    {
        IsRunning = true;
        OnTimerStarted.Invoke();
    }

    public static double TryGetTimeFromPrefs()
    {
        string ticksString = PlayerPrefs.GetString("Timer", "");
        if (ticksString == "") return 0;

        double time = 0;
        double.TryParse(ticksString, out time);

        return time;
    }

    private void Update()
    {
        if (!IsRunning) return;

        time += Time.deltaTime;

        OnTimerUpdated.Invoke(TimeSpan.FromSeconds(time));
    }

    public static TimeSpan GetTimeSpan()
    {
        return timerTimespan;
    }

    public static void ResetTimer()
    {
        IsRunning = false;
        PlayerPrefs.DeleteKey("Timer");
        time = 0;
    }

    private void OnApplicationQuit()
    {
        if (IsRunning)
        {
            PlayerPrefs.SetString("Timer", time.ToString());
            PlayerPrefs.Save();
        }
    }
}
