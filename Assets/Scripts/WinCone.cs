using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinCone : MonoBehaviour
{
    [SerializeField] UnityEvent OnPlayerTouch;
    bool playerHasTouched = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && ! playerHasTouched)
        {
            if (collision.transform.root.gameObject.TryGetComponent(out Engine engine))
            {
                engine.ToggleEngineRunning();
            }

            if (collision.transform.root.TryGetComponent(out WheelsController wheelsController))
            {
                wheelsController.HandBrake(true);
            }

            OnPlayerTouch.Invoke();
            playerHasTouched = true;
        }
    }
}
