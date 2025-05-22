using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSpin : MonoBehaviour
{
    [SerializeField] Vector3 axis;
    [SerializeField] float spinSpeed;

    private void Update()
    {
        transform.Rotate(axis, spinSpeed * Time.deltaTime);
    }
}
