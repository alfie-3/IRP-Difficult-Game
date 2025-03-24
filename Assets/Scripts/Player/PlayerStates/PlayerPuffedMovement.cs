using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPuffedMovement : BasePlayerMovementState
{
    public PlayerPuffedMovement(PlayerMovementStateContext context) : base(context) { }

    public override void EnterState()
    {
        Debug.Log("Puffed!");
    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {
        Context.MovementManager.Roll(Context.MovementManager.CameraAlignMovement(Context.Input.MovementInput));
    }

    public override void UpdateState()
    {

    }
}
