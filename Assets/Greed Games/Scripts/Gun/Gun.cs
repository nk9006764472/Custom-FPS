using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    public int RemainingBullets{get{return remainingBullets;} set{remainingBullets = value;}}
    public GunConfig GunConfig => _gunConfig;
    public GunMuzzleFlashConfig gunMuzzleFlashConfig => _gunMuzzleFlashConfig;
    public GunRecoilConfig GunRecoilConfig => _gunRecoilConfig;
    public PlayerController PlayerController => playerController;
    public GameObject Decal => _decal;
    public GameObject BulletTrail => _bulletTrail;
    public GameObject MuzzleFlash => _muzzleFlash;
    public Transform MuzzlePoint => _muzzlePoint;
    public float ElapsedRecoilCooldown {get{return elapsedRecoilCooldown;} set{elapsedRecoilCooldown = value;}}


    [SerializeField] private GunConfig _gunConfig;
    [SerializeField] private GunMuzzleFlashConfig _gunMuzzleFlashConfig;
    [SerializeField] private GunRecoilConfig _gunRecoilConfig;
    [SerializeField] private GunStateController _stateController;
    [SerializeField] private GameObject _decal;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private Transform _muzzlePoint;
    
    private int remainingBullets;
    private float elapsedBetweenShotCooldown = 0f;
    private float elapsedRecoilCooldown = 0f;
    private PlayerController playerController;


    public void Initialize(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    private void OnEnable() 
    {
        remainingBullets = _gunConfig.bulletCapacity;    
    }

    public void ShootBegin()
    {
        
    }

    public void ShootPressed()
    {

        if(remainingBullets > 0)
        {
            if(elapsedBetweenShotCooldown <= 0f)
            {
                _stateController.EnterState(GunStateType.FIRE);

                elapsedBetweenShotCooldown = 1/_gunConfig.fireRate;
            }
        }
        else
        {
            _stateController.EnterState(GunStateType.RELOAD);
        }
    }

    public void ShootEnd()
    {
        if(_stateController.CurrentState == _stateController.States[GunStateType.FIRE])
        {
            _stateController.EnterState(GunStateType.IDLE);
        }
    }

    private void Update() 
    {
        elapsedBetweenShotCooldown -= Time.deltaTime;    
        elapsedRecoilCooldown -= Time.deltaTime;

    }




    public void CameraRecoil(float _recoilSum)
    {
        float recoilSpreadY = Mathf.Pow(_recoilSum, _gunRecoilConfig._power) * _gunRecoilConfig._multipler;
        recoilSpreadY = Mathf.Clamp(recoilSpreadY, 0f, _gunRecoilConfig._maxAngleDeviationY);
        
        float maxRecoilSpreadX = _gunRecoilConfig._maxAngleDeviationX;
        float recoilSpreadX = Random.Range(-_gunRecoilConfig._angleDeviationX, _gunRecoilConfig._angleDeviationX);

        float currentMaxSpreadX = (recoilSpreadY / _gunRecoilConfig._maxAngleDeviationY) * maxRecoilSpreadX;

        recoilSpreadX = Mathf.Clamp(recoilSpreadX, -currentMaxSpreadX, currentMaxSpreadX);


        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(playerController.FPSCamHolder.transform.DOLocalRotate(new Vector3(playerController.FPSCamHolder.transform.localRotation.x - recoilSpreadY * 0.3f - 2f, 
        playerController.FPSCamHolder.transform.localRotation.y + recoilSpreadX,
        playerController.FPSCamHolder.transform.localRotation.z), 
        0.1f));
        mySequence.Append(playerController.FPSCamHolder.transform.DOLocalRotate(Vector3.zero, 0.2f));
        mySequence.Play();


    }
}
