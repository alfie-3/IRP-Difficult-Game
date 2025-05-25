using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] bool triggerOnce;
    public UnityEvent OnPlayerTrigger;
    bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnce && triggered) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggered = true;
            OnPlayerTrigger.Invoke();
        }
    }
}
