using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        ResetValues();
        SceneManager.LoadScene(0);
    }

    public void ResetAndQuit()
    {
        ResetValues();
        Application.Quit();
    }

    public void ResetValues()
    {
        Time.timeScale = 1.0f;
        CheckpointManager.ResetSavedCheckpoint();
        SpeedRunTimer.ResetTimer();
        RespawnHandler.ResetRespawns();
    }
}
