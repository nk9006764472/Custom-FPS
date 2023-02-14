using UnityEngine;

public class GunIdleState : GunBaseState
{
    public override void Initialize(GunStateController _controller)
    {
        type = GunStateType.IDLE;
        controller = _controller;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
