using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GunFireState : GunBaseState
{
    private float elapsedBetweenAutoShot = 0f;
    private float range = 200f;
    private float recoilSum = 0f;

    public override void Initialize(GunStateController _controller)
    {
        type = GunStateType.FIRE;
        controller = _controller;
    }

    public override void EnterState()
    {
        base.EnterState();

        elapsedBetweenAutoShot = 0f;

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

        controller.Gun.CameraRecoil(_recoilSum);

        Ray ray = controller.Gun.PlayerController.FPSCam.ScreenPointToRay(controller.Gun.PlayerController.CrossHair.position);
        ApplyRecoil(ref ray, _recoilSum);
        ShotInAccuracy(ref ray);

        RaycastHit hit;
        Vector3 hitPosition;
        if (Physics.Raycast(ray, out hit, range))
        {
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = ray.direction * range;
        }

        StartCoroutine(SpawnDecal(hit));
        StartCoroutine(SpawnTrail(hitPosition));
        StartCoroutine(SpawnFlash());
        controller.Gun.ElapsedRecoilCooldown = controller.Gun.GunRecoilConfig._recoilCoolDownTime;
    }

    private void ShotInAccuracy(ref Ray ray)
    {
        float spread = controller.Gun.GunConfig.firstShotSpread;

        ray.direction = Quaternion.AngleAxis(Random.Range(-spread, spread), transform.right) * ray.direction;
        ray.direction = Quaternion.AngleAxis(Random.Range(-spread, spread), transform.forward) * ray.direction;
    }

    private void ApplyRecoil(ref Ray ray, float _recoilSum)
    {
        //Vertical Recoil
        float recoilSpreadY = Mathf.Pow(_recoilSum, controller.Gun.GunRecoilConfig._power) * controller.Gun.GunRecoilConfig._multipler;
        recoilSpreadY = Mathf.Clamp(recoilSpreadY, 0f, controller.Gun.GunRecoilConfig._maxAngleDeviationY);
        ray.direction = Quaternion.AngleAxis(recoilSpreadY, transform.right) * ray.direction;

        //Horizontal Recoil
        float maxRecoilSpreadX = controller.Gun.GunRecoilConfig._maxAngleDeviationX;
        float recoilSpreadX = Random.Range(-controller.Gun.GunRecoilConfig._angleDeviationX, controller.Gun.GunRecoilConfig._angleDeviationX);

        float currentMaxSpreadX = (recoilSpreadY / controller.Gun.GunRecoilConfig._maxAngleDeviationY) * maxRecoilSpreadX;

        recoilSpreadX = Mathf.Clamp(recoilSpreadX, -currentMaxSpreadX, currentMaxSpreadX);
        ray.direction = Quaternion.AngleAxis(recoilSpreadX, transform.forward) * ray.direction;
    }

    private IEnumerator SpawnDecal(RaycastHit hit)
    {
        GameObject decal = PoolManager.Instance.GetItem(controller.Gun.Decal);
        decal.transform.position = hit.point + (hit.normal * 0.01f);
        decal.transform.rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
        yield return new WaitForSeconds(5f);
        PoolManager.Instance.ReturnItem(decal, controller.Gun.Decal.GetInstanceID());
    }

    private IEnumerator SpawnTrail(Vector3 hitPosition)
    {
        GameObject trail = PoolManager.Instance.GetItem(controller.Gun.BulletTrail);
        trail.transform.position = controller.Gun.MuzzlePoint.position;
        trail.transform.DOLocalMove(hitPosition, Vector3.Distance(controller.Gun.MuzzlePoint.position, hitPosition) / Random.Range(300f, 500f));
        yield return new WaitForSeconds(1f);
        PoolManager.Instance.ReturnItem(trail, controller.Gun.BulletTrail.GetInstanceID());
    }

    private IEnumerator SpawnFlash()
    {
        GameObject flash = PoolManager.Instance.GetItem(controller.Gun.MuzzleFlash);
        flash.transform.position = controller.Gun.MuzzlePoint.position;
        yield return new WaitForSeconds(1f);
        PoolManager.Instance.ReturnItem(flash, controller.Gun.MuzzleFlash.GetInstanceID());
    }
}
