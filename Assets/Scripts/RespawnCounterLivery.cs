using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RespawnCounterLivery : MonoBehaviour
{
    [SerializeField] TMP_Text[] liveryText;

    private void OnEnable()
    {
        RespawnHandler.OnRespawnCounterChanged += UpdateLivery;
    }

    private void OnDisable()
    {
        RespawnHandler.OnRespawnCounterChanged -= UpdateLivery;
    }

    private void UpdateLivery(int obj)
    {
        foreach (var item in liveryText)
        {
            item.text = obj.ToString();
        }
    }
}
