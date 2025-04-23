using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] float power = 100f;
    [SerializeField] float brakePower = 1000f;

    [SerializeField] WheelsController wheelsController;

    public void Throttle(float value)
    {
        wheelsController.Throttle(value * power);
    }

    public void Brake(float value)
    {
        wheelsController.Brake(value * brakePower);
    }
}
