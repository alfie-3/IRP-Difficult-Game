using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerInput _playerInput;

    [field: SerializeField] public Vector3 MovementInput {  get; private set; }

    public bool JumpHeldThisFrame = false;

    public Action<InputAction.CallbackContext> PuffedPerformed;

    public void Awake()
    {
        _playerInput = new();
        _playerInput.Enable();

        _playerInput.Default.Puff.performed += PuffedPerformed;
    }

    public void Update()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        Vector3 rawMovementInput = Vector2.ClampMagnitude(_playerInput.Default.Move.ReadValue<Vector2>(), 1);

        MovementInput = new(rawMovementInput.x, 0, rawMovementInput.y);

        JumpHeldThisFrame = _playerInput.Default.Jump.IsPressed();
    }
}
