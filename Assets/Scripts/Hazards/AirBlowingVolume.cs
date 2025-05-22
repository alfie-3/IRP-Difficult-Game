using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBlowingVolume : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] BoxCollider boxCollider;

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody == null) return;

        other.attachedRigidbody.AddForce(transform.forward * force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(transform.position, 0.5f);


        if (boxCollider != null)
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * boxCollider.bounds.size.magnitude);
    }
}
