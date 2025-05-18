using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class Engine : MonoBehaviour
{
    [SerializeField] WheelsController wheelsController;
    [field: Space]
    [field: SerializeField] public bool Running {  get; private set; }
    [field: SerializeField] public float Throttle;
    [field: SerializeField] public float EngineRPM;
    [SerializeField] float motorPower;
    [SerializeField] AnimationCurve horsePowerToRPM;
    [SerializeField] AnimationCurve breakCurve;
    [SerializeField] float idleRPM;
    [SerializeField] float redLine;
    [Space]
    [SerializeField] float brakePower = 1000f;
    [Space]
    [SerializeField] float differentialRatio = 0.1f;
    [SerializeField] Gear[] Gears;
    [SerializeField] float clutch = 0;
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

    public void UpdateClutch(float clutchInput)
    {
        clutch = Mathf.Lerp(clutch, 1 - clutchInput, Time.deltaTime * 3);
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

    public float CalculateTorque()
    {
        float torque = 0;

        if (Running && !neutral)
        {
            if (clutch < 0.1f)
            { 

            }
            else
            {
                float wheelRPM = wheelsController.GetDriveWheelAverageRPM();

                wheelRPM *= CurrentGear.GearRatio * differentialRatio;
                EngineRPM = Mathf.Lerp(EngineRPM, Mathf.Max(idleRPM - 100, redLine * Throttle), Time.deltaTime * 3);
                torque = (horsePowerToRPM.Evaluate(EngineRPM / redLine) * motorPower / EngineRPM) * CurrentGear.GearRatio * differentialRatio * 5252f * clutch;
                Debug.Log(torque);
            }
        }

        return torque;
    }

    public void ApplyThrottle()
    {
        wheelsController.Throttle(CalculateTorque());
    }

    public void Brake(float value)
    {
        if (value == 0)
        {
            wheelsController.Brake(0);
        }

        if (!neutral && clutch > 0.1f)
        {
            if (EngineRPM < idleRPM) return;

            wheelsController.Brake(value * (breakCurve.Evaluate(EngineRPM / redLine) * brakePower));
        }
        else
        {
            wheelsController.Brake(value * brakePower);
        }
    }
}

[System.Serializable]
public struct Gear
{
    [field: SerializeField] public float GearRatio { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
}