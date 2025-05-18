using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsController : MonoBehaviour
{
    [field: SerializeField] public Wheel[] Wheels {  get; private set; }
    [Space]
    [SerializeField] bool handBrake;
    [Header("Steering")]
    [SerializeField] float steeringSpeed;
    [SerializeField] float maxAngle = 90f;
    [SerializeField] float offset = 0;
    float currentSteeringAngle;

    public void Throttle(float power)
    {
        foreach (var wheel in Wheels)
        {
            if (!wheel.Drive) continue;

            wheel.ProvideMotorTorque(power);
        }
    }

    public void Brake(float power)
    {
        foreach (var wheel in Wheels)
        {
            wheel.ProvideBrakeTorque(power);
        }
    }

    public void HandBrake(bool value)
    {
        foreach (var wheel in Wheels)
        {
            handBrake = value;
            wheel.ProvideHandbrake(value);
        }
    }

    public void Steer(float steeringInput)
    {
        foreach (var wheel in Wheels)
        {
            if (!wheel.Steering) continue;

            currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, (steeringInput * maxAngle + offset), steeringSpeed * Time.deltaTime);
            wheel.ProvideSteering(currentSteeringAngle);
        }
    }
    public float GetDriveWheelAverageRPM()
    {
        float totalRPM = 0;
        int driveWheels = 0;

        foreach (Wheel wheel in Wheels)
        {
            if (wheel.Drive)
            {
                totalRPM += wheel.WheelCollider.rpm;
                driveWheels++;
            }
        }

        if (driveWheels == 0) return 0;

        return totalRPM / driveWheels;
    }
}
