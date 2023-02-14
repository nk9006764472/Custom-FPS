using UnityEngine;

public class GunFireState : GunBaseState
{
    private float elapsedBetweenAutoShot = 0f;
    private Transform crosshair;
    private float range = 200f;
    private int bulletInARowCount = 0;
    private float recoilSum = 0f;

    public override void Initialize(GunStateController _controller)
    {
        type = GunStateType.FIRE;
        controller = _controller;

        crosshair = controller.Gun.PlayerController.CrossHair;
    }

    public override void EnterState()
    {
        base.EnterState();

        elapsedBetweenAutoShot = 0f;
        bulletInARowCount = 0;

        if(controller.Gun.GunConfig.fireType == GunFireType.SINGLE)
        {
            Shoot(CalculateRecoilSum());
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();


        if(controller.Gun.GunConfig.fireType == GunFireType.AUTO)
        {
            elapsedBetweenAutoShot -= Time.deltaTime;
            if(elapsedBetweenAutoShot <= 0f)
            {
                Shoot(CalculateRecoilSum());

                elapsedBetweenAutoShot = 1/controller.Gun.GunConfig.fireRate;
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private float CalculateRecoilSum()
    {   
        if(controller.Gun.ElapsedRecoilCooldown <= 0)
        {
            recoilSum = 0f;
        }
        else
        {
            recoilSum += controller.Gun.ElapsedRecoilCooldown;
        }

        return recoilSum;
    }

    private void Shoot(float _recoilSum)
    {
        controller.Gun.RemainingBullets--;
        bulletInARowCount++;

        Ray ray = controller.Gun.PlayerController.FPSCam.ScreenPointToRay(crosshair.position);
        ApplyRecoil(ref ray, _recoilSum);
        //ShotInAccuracy(ref ray);

        RaycastHit hit;
        Vector3 hitPosition;
        if(Physics.Raycast(ray, out hit, range))
        {
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = ray.direction * range;
        }

        Instantiate(controller.Gun.Decal, hit.point + (hit.normal * 0.01f), Quaternion.FromToRotation(Vector3.forward, -hit.normal));
        controller.Gun.ElapsedRecoilCooldown = controller.Gun.GunRecoilConfig._recoilCoolDownTime;
    }



    private void ShotInAccuracy(ref Ray ray)
    {
        ray.direction = Quaternion.AngleAxis(Random.Range(0f, controller.Gun.GunConfig.firstShotSpread),
         new Vector3(0f, Random.Range(-1f, 1f), Random.Range(-1f, 1f))) * ray.direction;
    }

    private void ApplyRecoil(ref Ray ray, float _recoilSum)
    {
        float recoilSpreadY = Mathf.Pow(_recoilSum, controller.Gun.GunRecoilConfig._power);
        ray.direction = Quaternion.AngleAxis(recoilSpreadY, new Vector3(0f, 0f, 1f)) * ray.direction;
    }
}
