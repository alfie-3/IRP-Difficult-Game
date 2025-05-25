using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] List<Checkpoint> checkpoints = new List<Checkpoint>();
    public static List<Checkpoint> CheckPointList = new();

    public static int CurrentCheckpointIndex = 0;
    public static Checkpoint CurrentCheckpoint => CheckPointList[CurrentCheckpointIndex];

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        CurrentCheckpointIndex = 0;
        CheckPointList = null;
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentCheckpoint"))
        {
            CurrentCheckpointIndex = PlayerPrefs.GetInt("CurrentCheckpoint");
        }

        CheckPointList = checkpoints;
    }

    public static void CheckPointReached(Checkpoint checkPoint)
    {
        for (int i = 0; i < CheckPointList.Count; i++)
        {
            if (CheckPointList[i] == checkPoint)
            {
                if (i == CurrentCheckpointIndex) break;

                CurrentCheckpointIndex = i;

                PlayerPrefs.SetInt("CurrentCheckpoint", CurrentCheckpointIndex);
                PlayerPrefs.Save();

                break;
            }
        }
    }

    [ContextMenu("ResetSavedCheckpoint")]
    public static void ResetSavedCheckpoint()
    {
        PlayerPrefs.SetInt("CurrentCheckpoint", 0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("CurrentCheckpoint", CurrentCheckpointIndex);
        PlayerPrefs.Save();
    }
}
