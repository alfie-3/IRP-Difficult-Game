using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [field: SerializeField] public WheelCollider WheelCollider {  get; private set; }
    [SerializeField] GameObject wheelMesh;
    [Space]
    [field: SerializeField] public bool Drive;
    [field: SerializeField] public bool Steering;


    void Update()
    {
        UpdateWheelMesh();
    }

    public void UpdateWheelMesh()
    {
        Vector3 position;
        Quaternion rotation;

        WheelCollider.GetWorldPose(out position, out rotation);

        wheelMesh.transform.SetPositionAndRotation(position, rotation);
    }

    public void ProvideMotorTorque(float force)
    {
        WheelCollider.motorTorque = force;
    }

    public void ProvideBrakeTorque(float force)
    {
        WheelCollider.brakeTorque = force;
    }

    public void ProvideHandbrake(bool value)
    {
        if (value)
        {
            WheelCollider.brakeTorque = 2000;
        }
        else
        {
            WheelCollider.brakeTorque = 0;
        }
    }

    public void ResetVelocity()
    {
        WheelCollider.motorTorque = 0;
        WheelCollider.brakeTorque = 0;

        WheelCollider.rotationSpeed = 0;
    }

    public void ProvideSteering(float steeringAngle)
    {
        float turnAngle = steeringAngle;
        WheelCollider.steerAngle = turnAngle;
    }
}
