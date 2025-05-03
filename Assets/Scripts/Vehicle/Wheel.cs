using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] WheelCollider wheelCollider;
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

        wheelCollider.GetWorldPose(out position, out rotation);

        wheelMesh.transform.SetPositionAndRotation(position, rotation);
    }

    public void ProvideMotorTorque(float force)
    {
        wheelCollider.motorTorque = force;
    }

    public void ProvideBrakeTorque(float force)
    {
        wheelCollider.brakeTorque = force;
    }

    public void ProvideHandbrake(bool value)
    {
        if (value)
        {
            wheelCollider.brakeTorque = 2000;
        }
        else
        {
            wheelCollider.brakeTorque = 0;
        }
    }

    public void ResetVelocity()
    {
        wheelCollider.motorTorque = 0;
        wheelCollider.brakeTorque = 0;

        wheelCollider.rotationSpeed = 0;
    }

    public void ProvideSteering(float steeringAngle)
    {
        float turnAngle = steeringAngle;
        wheelCollider.steerAngle = turnAngle;
    }
}
