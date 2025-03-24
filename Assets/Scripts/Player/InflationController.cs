using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflationController : MonoBehaviour
{
    [SerializeField] CapsuleCollider _capsuleCollider;
    [SerializeField] Rigidbody _rigidbody;
    [Space]
    [SerializeField] float _duration = 0.25f;
    [Header("Inflated")]
    [SerializeField] InflationStateVariables _inflatedStateVariables;
    [Space]
    [Header("Deflated")]
    [SerializeField] InflationStateVariables _deflatedStateVariables;

    private void Awake()
    {
        GetComponent<PlayerMovementManager>().OnPuffedChanged += OnPuffedChanged;
    }

    public void OnPuffedChanged(bool state)
    {
        InflationStateVariables inflationStateVariables = state == true ? _inflatedStateVariables : _deflatedStateVariables;

        DOVirtual.Float(_capsuleCollider.radius, inflationStateVariables.Radius, _duration, (current) => { _capsuleCollider.radius = current; });
        DOVirtual.Float(_capsuleCollider.height, inflationStateVariables.Height, _duration, (current) => { _capsuleCollider.height = current; });
        DOVirtual.Float(_rigidbody.drag, inflationStateVariables.Drag, _duration, (current) => { _rigidbody.drag = current; });
    }
}

[System.Serializable]
public class InflationStateVariables
{
    public float Radius;
    public float Height;
    [Space]
    public float Drag;
}