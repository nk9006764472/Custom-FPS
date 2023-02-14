using UnityEngine;

public class Gun : MonoBehaviour
{
    public int RemainingBullets{get{return remainingBullets;} set{remainingBullets = value;}}
    public GunConfig GunConfig => _gunConfig;
    public GunMuzzleFlashConfig gunMuzzleFlashConfig => _gunMuzzleFlashConfig;
    public GunRecoilConfig GunRecoilConfig => _gunRecoilConfig;
    public PlayerController PlayerController => playerController;
    public GameObject Decal => _decal;
    public float ElapsedRecoilCooldown {get{return elapsedRecoilCooldown;} set{elapsedRecoilCooldown = value;}}


    [SerializeField] private GunConfig _gunConfig;
    [SerializeField] private GunMuzzleFlashConfig _gunMuzzleFlashConfig;
    [SerializeField] private GunRecoilConfig _gunRecoilConfig;
    [SerializeField] private GunStateController _stateController;
    [SerializeField] private GameObject _decal;
    
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

}
