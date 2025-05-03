using UnityEngine;

public class OutOfBoundsZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.TryGetComponent(out PlayerInputController input))
        {
            RespawnHandler.RespawnPlayer(other.transform.root.gameObject);
        }
    }
}
