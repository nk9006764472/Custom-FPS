using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Transform CrossHair => _crosshair;
    public Transform FPSCamHolder => _fpsCameraHolder;
    public Camera FPSCam => _fpsCam;
    public Camera GunCam => _gunCam;

    [SerializeField] private float _verticalMoveSpeed;
    [SerializeField] private float _horizontalMoveSpeed;

    [SerializeField] private float _altVerticalMoveSpeed;
    [SerializeField] private float _altHorizontalMoveSpeed;

    [SerializeField] private float _XLookSensitivity;
    [SerializeField] private float _YLookSensitivity;
    [SerializeField] private float _gravityForce = -9.81f;
    [SerializeField] private float _jumpHeight = 1f;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _fpsCameraHolder;
    [SerializeField] private Transform _gunHolder;
    [SerializeField] private Camera _fpsCam;
    [SerializeField] private Camera _gunCam;
    [SerializeField] private Mobile_Input mobileInput;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Gun _currentGun;
    [SerializeField] private Transform _crosshair;


    private InputHandler inputHandler;
    private Vector2 lookRotation;
    private Vector3 downwardVelocity;

    private void Awake() 
    {
        InitializeInputHandler();
    }

    private void InitializeInputHandler()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            inputHandler = gameObject.AddComponent<Mobile_InputHandler>();
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            inputHandler = gameObject.AddComponent<PC_InputHandler>();
        }
        else
        Debug.Log("Not Supported in this device.");

       inputHandler.Initialize();
       _currentGun.Initialize(this);
    }

    private void Update() 
    {
        UpdateMovement();
        UpdateLook();
        CheckShootInput();
        CheckGunPickInput();
        CheckGunDropInput();
    }

    private void CheckShootInput()
    {
        if (_currentGun == null)
            return;

        if(inputHandler.GetShootPressed())
        {
            _currentGun.ShootPressed();
        }

        if(inputHandler.GetShootBegin())
        {
            _currentGun.ShootBegin();
        }

        if(inputHandler.GetShootEnd())
        {
            _currentGun.ShootEnd();
        }
    }

    private void UpdateMovement()
    {
        float moveSpeedX, moveSpeedZ;
        if(inputHandler.GetAlternateWalk())
        {
            moveSpeedX = _altHorizontalMoveSpeed;
            moveSpeedZ = _altVerticalMoveSpeed;
        }
        else
        {
            moveSpeedX = _horizontalMoveSpeed;
            moveSpeedZ = _verticalMoveSpeed;
        }

        _controller.Move((moveSpeedX * inputHandler.GetHorizontalMovement() * transform.right+
          moveSpeedZ * inputHandler.GetVerticalMovement() * transform.forward) * Time.deltaTime);

        if(IsGrounded())
        {
            if(inputHandler.GetJump())
            {
                downwardVelocity.y = Mathf.Sqrt(-2 * _gravityForce * _jumpHeight);
            }
            downwardVelocity.y += _gravityForce/10f * Time.deltaTime;
        }
        else
        {
            downwardVelocity.y += _gravityForce * Time.deltaTime;
        }
        _controller.Move(downwardVelocity * Time.deltaTime);
    }

    private void UpdateLook()
    {
        lookRotation.x += inputHandler.GetHorizontalLook() * _XLookSensitivity;
        lookRotation.y = Mathf.Clamp(lookRotation.y + inputHandler.GetVerticalLook() * _YLookSensitivity, -85f, 85f);

        _fpsCam.transform.localRotation = Quaternion.Euler(-lookRotation.y, 0f, 0f);
        _gunCam.transform.localRotation = Quaternion.Euler(-lookRotation.y, 0f, 0f);
        gameObject.transform.localRotation = Quaternion.Euler(0f, lookRotation.x, 0f);
    }

    private bool IsGrounded()
    {
        return  Physics.CheckSphere(new Vector3(transform.localPosition.x, transform.localPosition.y - _controller.height/2, transform.localPosition.z),
         0.3f, groundLayerMask);
    }

    private void CheckGunPickInput()
    { 
        
    }

    private void CheckGunDropInput()
    {
        if (_currentGun == null) return;

        if (inputHandler.GetGunDrop())
        {
            Rigidbody gunRb = _currentGun.GetComponent<Rigidbody>();
            gunRb.constraints = RigidbodyConstraints.None;
            _currentGun.transform.parent = null;
            gunRb.AddForce(transform.forward * Random.Range(120f, 180f));
            gunRb.transform.DORotate(new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(-10f, 10f)), 0.6f);
            _currentGun = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Gun>(out Gun gun))
        {
            _currentGun = gun;
            _currentGun.transform.parent = _gunHolder;
            _currentGun.transform.localPosition = Vector3.zero;
            Rigidbody gunRb = _currentGun.GetComponent<Rigidbody>();
            gunRb.constraints = RigidbodyConstraints.FreezeAll;
            gun.SetPositionAndRotation();
        }
    }
}


