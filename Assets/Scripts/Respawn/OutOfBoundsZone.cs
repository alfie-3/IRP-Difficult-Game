using UnityEngine;

public class OutOfBoundsZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.TryGetComponent(out PlayerInputController input))
        {
            Debug.Log("Respawning Player");

            foreach (Rigidbody rb in other.transform.root.GetComponentsInChildren<Rigidbody>())
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            other.transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.root.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            if (Checkpoint.CurrentCheckpoint != null)
            {
                other.transform.root.GetComponent<Rigidbody>().MovePosition(Checkpoint.CurrentCheckpoint.transform.position);
                other.transform.root.GetComponent<Rigidbody>().MoveRotation(Checkpoint.CurrentCheckpoint.transform.rotation);
            }

        }
    }
}
