using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [field: SerializeField] public WheelCollider WheelCollider {  get; private set; }
    [SerializeField] GameObject wheelMesh;
    [Space]
    [field: SerializeField] public bool Drive;
    [field: SerializeField] public bool Steering;

    float baseStiffness;
    float baseSidewaysFriction;

    private void Awake()
    {
        if (WheelCollider)
        {
            baseStiffness = WheelCollider.forwardFriction.stiffness;
            baseSidewaysFriction = WheelCollider.sidewaysFriction.stiffness;
        }
    }

    void Update()
    {
        UpdateWheelMesh();
    }

    private void FixedUpdate()
    {
        UpdateWheelPhysics();
    }

    public void UpdateWheelMesh()
    {
        Vector3 position;
        Quaternion rotation;

        WheelCollider.GetWorldPose(out position, out rotation);

        wheelMesh.transform.SetPositionAndRotation(position, rotation);
    }

    public void UpdateWheelPhysics()
    {
        if (!WheelCollider) return;

        WheelHit wheelHit;
        if (WheelCollider.GetGroundHit(out wheelHit))
        {
            WheelFrictionCurve frictionCurve = WheelCollider.forwardFriction;
            frictionCurve.stiffness = wheelHit.collider.material.staticFriction * baseStiffness;
            WheelCollider.forwardFriction = frictionCurve;

            WheelFrictionCurve sFriction = WheelCollider.sidewaysFriction;
            sFriction.stiffness = wheelHit.collider.material.staticFriction * baseSidewaysFriction;
            WheelCollider.sidewaysFriction = sFriction;

        }
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
