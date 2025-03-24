using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishMovement : BasePlayerMovementState
{
    public PlayerFishMovement(PlayerMovementStateContext context) : base(context) { }

    public override void EnterState()
    {
        Debug.Log("Deflated");
    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {

    }
}
