using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent OnCheckpointReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.TryGetComponent(out PlayerInputController input))
        {
            SetCheckPoint();
        }
    }

    public void SetCheckPoint()
    {
        if (CheckpointManager.CurrentCheckpoint == this) return;

        Debug.Log("Updating Checkpoint");

        CheckpointManager.CheckPointReached(this);
        OnCheckpointReached.Invoke();
    }
}
