using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;

public class PlayerInputController : MonoBehaviour
{
    PlayerInput _playerInput;

    [field: SerializeField] public float SteeringInput {  get; private set; }
    [field: SerializeField] public float RockInput { get; private set; }
    [field: Space]
    [field: SerializeField] public Engine Engine {  get; private set; }

    public Action<InputAction.CallbackContext> OnAccelerate = delegate { };
    public Action<InputAction.CallbackContext> OnBrake = delegate { };
    public Action<InputAction.CallbackContext> OnHandbrake = delegate { };

    public void OnEnable()
    {
        _playerInput = new();
        _playerInput.Enable();

        _playerInput.Default.Accelerate.performed += (ctx) => OnAccelerate.Invoke(ctx);
        _playerInput.Default.Brake.performed += (ctx) => OnBrake.Invoke(ctx);

        _playerInput.Default.Handbrake.started += (ctx) => OnHandbrake.Invoke(ctx); 
        _playerInput.Default.Handbrake.canceled += (ctx) => OnHandbrake.Invoke(ctx);

        _playerInput.Default.Respawn.performed += (ctx) => RespawnHandler.RespawnPlayer(gameObject);
    }

    public void OnDisable()
    {
        _playerInput.Dispose();
    }

    public void Update()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        SteeringInput = _playerInput.Default.Steer.ReadValue<float>();
        RockInput = _playerInput.Default.Rock.ReadValue<float>();
    }
}
