using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] WheelsController wheelsController;
    [Space]
    [SerializeField] float brakePower = 1000f;
    [Space]
    [SerializeField] Gear[] Gears;
    [SerializeField] bool neutral = true;
    [field: SerializeField] public int CurrentGearIndex { get; private set; }

    public Gear CurrentGear
    {
        get
        {
            if (CurrentGearIndex == -1) return Gears[0];
            if (CurrentGearIndex > Gears.Length - 1) return Gears[^1];

            return Gears[CurrentGearIndex];
        }
    }

    public void ChangeGear(int newGear)
    {
        if (newGear > Gears.Length) return;

        neutral = newGear == -1;

        if (neutral) return;

        CurrentGearIndex = newGear;
    }

    public void Throttle(float value)
    {
        if (neutral) return;

        wheelsController.Throttle(value * CurrentGear.Power);
    }

    public void Brake(float value)
    {
        wheelsController.Brake(value * brakePower);
    }
}

[System.Serializable]
public struct Gear
{
    [field: SerializeField] public float Power { get; private set; }
}