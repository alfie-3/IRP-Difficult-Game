using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerInput _playerInput;

    [field: SerializeField] public float SteeringInput {  get; private set; }
    [field: Space]
    [field: SerializeField] public Engine Engine {  get; private set; }

    public Action<InputAction.CallbackContext> OnAccelerate = delegate { };
    public Action<InputAction.CallbackContext> OnBrake = delegate { };
    public Action<InputAction.CallbackContext> OnHandbrake = delegate { };

    public void Awake()
    {
        _playerInput = new();
        _playerInput.Enable();

        _playerInput.Default.Accelerate.performed += OnAccelerate;
        _playerInput.Default.Brake.performed += OnBrake;

        _playerInput.Default.Handbrake.started += OnHandbrake;
        _playerInput.Default.Handbrake.canceled += OnHandbrake;

        CursorUtils.LockAndHideCusor();
    }

    public void Update()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        SteeringInput = _playerInput.Default.Steer.ReadValue<float>();
    }
}
