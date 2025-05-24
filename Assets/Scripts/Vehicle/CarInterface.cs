using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInterface : MonoBehaviour
{
    [SerializeField] PlayerInputController Input;
    [SerializeField] Engine engine;
    [SerializeField] WheelsController wheelsController;
    [SerializeField] CarRocker carRocker;

    private void Awake()
    {
        if (Input == null) Input = GetComponent<PlayerInputController>();

        Input.OnAccelerate += PressAccelerator;
        Input.OnBrake += PressBrake;
        Input.OnHandbrake += HandBrake;

    }

    private void Update()
    {
        wheelsController.Steer(Input.SteeringInput);
        carRocker.RockCar(Input.RockInput);
    }

    public void ChangeGear(int newGear)
    {
        //engine.ChangeGear(newGear);
    }

    private void PressBrake(InputAction.CallbackContext context)
    {
        if (engine == null) return;

        float brakePower = context.ReadValue<float>();

        engine.Brake = brakePower;
    }

    private void HandBrake(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            wheelsController.HandBrake(true);

        }
        else if (context.canceled)
        {
            wheelsController.HandBrake(false);
        }
    }

    private void PressAccelerator(InputAction.CallbackContext context)
    {
        if (engine == null) return;

        float throttlePower = context.ReadValue<float>();

        engine.Throttle = throttlePower;
    }
}
