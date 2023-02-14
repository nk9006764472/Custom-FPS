using UnityEngine;

public class GunReloadState : GunBaseState
{

    private float reloadTimerElapsed = 0f;
    private float reloadTime;

    public override void Initialize(GunStateController _controller)
    {
        type = GunStateType.RELOAD;
        controller = _controller;

    }

    public override void EnterState()
    {
        base.EnterState();
        //play reload animaiton.

        reloadTime = controller.Gun.GunConfig.reloadTime;
        reloadTimerElapsed = reloadTime;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        reloadTimerElapsed -= Time.deltaTime;
        if(reloadTimerElapsed <= 0f)
        {
            controller.EnterState(GunStateType.IDLE);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        controller.Gun.RemainingBullets = controller.Gun.GunConfig.bulletCapacity;
    }
}
