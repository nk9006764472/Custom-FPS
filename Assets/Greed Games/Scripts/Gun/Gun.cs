    using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunConfig gunConfig;
    [SerializeField] private GunMuzzleFlashConfig gunMuzzleFlashConfig;

    private float coolDownElapsed = 0f;
    private bool singleFireable = true;
    private int remainingBulletCount;
    private float reloadElapsed = 0f;

    private void Awake() 
    {
        Initialize();    
    }

    public void Initialize()
    {
        remainingBulletCount = gunConfig.bulletCapacity;
    }

    public void ShootPressed()
    {
        switch(gunConfig.fireType)
        {
            case GunFireType.AUTO : 
            {
                if(coolDownElapsed <= 0f)
                {
                    Shoot();
                    coolDownElapsed = 1/gunConfig.fireRate;
                }
                break;
            }
            case GunFireType.SINGLE :
            {
                if(singleFireable == true)
                {
                    Shoot();
                    singleFireable = false;
                }
                break;
            } 
            case GunFireType.BURST : 
            {

                break;
            }
        }

        if(remainingBulletCount <= 0)
        {
            Reload();
        }
    }

    public void ShootExit()
    {
        singleFireable = true;
    }

    private void Update() 
    {
        if(coolDownElapsed > 0f)    
        {
            coolDownElapsed  -= Time.deltaTime;
        }
    }

    private void Reload()
    {
    }


    private void Shoot()
    {
        remainingBulletCount--;

        Debug.Log("Shooting");

    }
}
