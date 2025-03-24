public abstract class BasePlayerMovementState
{
    protected PlayerMovementStateContext Context;

    public BasePlayerMovementState(PlayerMovementStateContext context)
    {
        Context = context;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}

public class PlayerMovementStateContext
{
    public PlayerMovementManager MovementManager;
    public PlayerInputController Input;
    public PlayerCameraManager CameraManager;

    public PlayerMovementStateContext(PlayerMovementManager movementManager, PlayerInputController input, PlayerCameraManager camManager)
    {
        MovementManager = movementManager;
        Input = input;
        CameraManager = camManager;
    }


}