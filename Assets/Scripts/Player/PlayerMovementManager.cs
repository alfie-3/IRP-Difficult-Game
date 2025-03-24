using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//iHeartGameDev
//https://www.youtube.com/watch?v=kV06GiJgFhc

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovementManager : MonoBehaviour
{
    private PlayerInputController _playerInput;
    private PlayerCameraManager _cameraManager;
    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;

    private BasePlayerMovementState _currentState;

    private PlayerPuffedMovement _puffedMovement;
    private PlayerFishMovement _fishMovement;

    bool _isGrounded = false;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] float _groundDetectionRadius = 0.52f;
    private Vector3 _averageGroundNormal;
    [Space]
    [SerializeField] PlayerParameters _playerParameters;
    [SerializeField] bool _puffed;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody>();
        _cameraManager = GetComponent<PlayerCameraManager>();
        _sphereCollider = GetComponent<SphereCollider>();

        PlayerMovementStateContext context = new(this, _playerInput, _cameraManager);

        _puffedMovement = new PlayerPuffedMovement(context);
        _fishMovement = new PlayerFishMovement(context);

        _currentState = _fishMovement;

        _playerInput.PuffedPerformed += TogglePuff;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contactCount == 0) return;

        Vector3 contactsAverage = Vector3.zero;

        foreach(ContactPoint contact in collision.contacts)
        {
            contactsAverage += contact.normal;
        }

        contactsAverage.Normalize();
        
        _averageGroundNormal = contactsAverage;
        _rigidbody.AddForce(-_averageGroundNormal * _playerParameters.StickStrength, ForceMode.Acceleration);
    }

    public void TogglePuff(InputAction.CallbackContext context) {
        if (_puffed)
        {
            _puffed = false;
            SwitchState(_fishMovement);
        }
        else
        {
            _puffed = true;
            SwitchState(_puffedMovement);
        }
    }

    public void SwitchState(BasePlayerMovementState newMovementState)
    {
        _currentState.ExitState();
        _currentState = newMovementState;
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
        Debug.DrawRay(transform.position, _averageGroundNormal, Color.yellow);

        _isGrounded = CheckGrounded();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateState();
    }

    public Vector3 CameraAlignMovement(Vector3 movementVector)
    {
        Vector3 newDirection = movementVector.x * _cameraManager.CameraTransform.right + movementVector.z * _cameraManager.CameraTransform.forward;

        return newDirection;
    }

    public void Roll(Vector3 direction, float torque = 0)
    {
        if (torque == 0)
            torque = _playerParameters.RollTorque;


        //direction = direction.ProjectOntoPlane(_averageGroundNormal);
        direction = Vector3.Cross(direction, -Vector3.up);

        Debug.DrawRay(transform.position, direction, Color.red);

        _rigidbody.AddTorque(direction * torque, ForceMode.Acceleration);
    }

    public bool CheckGrounded()
    {
        if (Physics.CheckSphere(transform.position, _groundDetectionRadius, _groundMask))
        {
            _rigidbody.useGravity = false;
            return true;
        }

        _rigidbody.useGravity = true;
        return false;
    }

    public void Jump()
    {

    }

    private void OnDrawGizmos()
    {
        if (_rigidbody)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _rigidbody.velocity);
        }
    }
}
