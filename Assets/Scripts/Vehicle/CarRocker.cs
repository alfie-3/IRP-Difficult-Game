using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CarRocker : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [Space]
    [SerializeField] float rockAmount = 0.3f;
    [SerializeField] float rockSpeed = 4;

    public void RockCar(float input)
    {
        float torque = rockAmount * -input;
        rb.AddTorque(transform.forward * torque, ForceMode.VelocityChange);
    }
}
