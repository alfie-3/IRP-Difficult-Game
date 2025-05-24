using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSpinRigidbody : MonoBehaviour
{
    [SerializeField] Vector3 axis;
    [SerializeField] float spinSpeed;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.AngleAxis(spinSpeed, axis * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * rotation);
    }
}
