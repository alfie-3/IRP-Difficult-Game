using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint CurrentCheckpoint;

    public UnityEvent OnCheckpointReached;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        CurrentCheckpoint = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.TryGetComponent(out PlayerInputController input))
        {
            SetCheckPoint();
        }
    }

    public void SetCheckPoint()
    {
        if (CurrentCheckpoint == this) return;

        Debug.Log("Updating Checkpoint");

        CurrentCheckpoint = this;
        OnCheckpointReached.Invoke();
    }
}
