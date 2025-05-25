using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;

public class RespawnHandler : MonoBehaviour
{
    public static int Respawns = 0;
    public static Action<int> OnRespawnCounterChanged = delegate { };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        Respawns = 0;
    }

    private void Start()
    {
        int respawns = PlayerPrefs.GetInt("Respawns", 0);
        Respawns = respawns;
        OnRespawnCounterChanged.Invoke(Respawns);
    }

    public static void RespawnPlayer(GameObject Player, bool countAsRespawn = false)
    {
        Debug.Log("Respawning Player");

        foreach (Rigidbody rb in Player.transform.root.GetComponentsInChildren<Rigidbody>())
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        foreach (Wheel wheel in Player.transform.root.GetComponentsInChildren<Wheel>())
        {
            wheel.ResetVelocity();
        }

        Player.transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.transform.root.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (CheckpointManager.CurrentCheckpoint != null)
        {
            Player.transform.root.GetComponent<Rigidbody>().MovePosition(CheckpointManager.CurrentCheckpoint.transform.position);
            Player.transform.root.GetComponent<Rigidbody>().MoveRotation(CheckpointManager.CurrentCheckpoint.transform.rotation);
        }

        if (countAsRespawn)
        {
            Respawns++;
            OnRespawnCounterChanged.Invoke(Respawns);
        }
    }

    public static void ResetRespawns()
    {
        Respawns = 0;
        PlayerPrefs.DeleteKey("Respawns");
        OnRespawnCounterChanged.Invoke(Respawns);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Respawns", Respawns);
        PlayerPrefs.Save();
    }
}