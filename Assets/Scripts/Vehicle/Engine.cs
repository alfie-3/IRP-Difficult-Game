using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] WheelsController wheelsController;
    [field: Space]
    [field: SerializeField] public bool Running {  get; private set; }
    [field: SerializeField] public float Throttle;
    [Space]
    [SerializeField] float brakePower = 1000f;
    [Space]
    [SerializeField] Gear[] Gears;
    [SerializeField] bool neutral = true;
    [field: SerializeField] public int CurrentGearIndex { get; private set; }

    public Action<bool> OnChangeCarRunnng = delegate { };

    public Gear CurrentGear
    {
        get
        {
            if (CurrentGearIndex == -1) return Gears[0];
            if (CurrentGearIndex > Gears.Length - 1) return Gears[^1];

            return Gears[CurrentGearIndex];
        }
    }

    public float Speed;
    public float SpeedClamped;

    private void Update()
    {
        Speed = wheelsController.Wheels[0].WheelCollider.rpm * wheelsController.Wheels[0].WheelCollider.radius * 2f * Mathf.PI / 10;
        SpeedClamped = Mathf.Lerp(SpeedClamped, Speed, Time.deltaTime);

        ApplyThrottle();
    }

    public float GetSpeedRatio()
    {
        var throttle = Mathf.Clamp(Throttle, 0.5f, 1);
        return SpeedClamped * throttle / CurrentGear.MaxSpeed;
    }

    public void ToggleEngineRunning()
    {
        Running = !Running;

        OnChangeCarRunnng.Invoke(Running);
    }

    public void ChangeGear(int newGear)
    {
        if (newGear > Gears.Length) return;

        neutral = newGear == -1;

        if (neutral)
        {
            wheelsController.Throttle(0);
        }

        CurrentGearIndex = newGear;
    }

    public void ApplyThrottle()
    {
        if (neutral || !Running || Speed > CurrentGear.MaxSpeed || Speed < -CurrentGear.MaxSpeed) 
        {
            wheelsController.Throttle(0);
            return;
        }

        wheelsController.Throttle(Throttle * CurrentGear.Power);
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
    [field: SerializeField] public float MaxSpeed { get; private set; }
}