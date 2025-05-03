using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint CurrentCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.TryGetComponent(out PlayerInputController input))
        {
            SetCheckPoint();
        }
    }

    public void SetCheckPoint()
    {

        Debug.Log("Updating Checkpoint");

        CurrentCheckpoint = this;
    }
}
