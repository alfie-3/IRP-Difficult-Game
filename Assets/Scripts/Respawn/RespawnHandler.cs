using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;

public class RespawnHandler : MonoBehaviour
{
    public static void RespawnPlayer(GameObject Player)
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

        if (Checkpoint.CurrentCheckpoint != null)
        {
            Player.transform.root.GetComponent<Rigidbody>().MovePosition(Checkpoint.CurrentCheckpoint.transform.position);
            Player.transform.root.GetComponent<Rigidbody>().MoveRotation(Checkpoint.CurrentCheckpoint.transform.rotation);
        }
    }
}