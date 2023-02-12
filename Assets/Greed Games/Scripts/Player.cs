using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private Gun _currentGun;
    private InputHandler inputHandler;

    private void Awake() 
    {
        InitializeInputHandler();
    }

    private void InitializeInputHandler()
    {
       //if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
       //{
            inputHandler = gameObject.AddComponent<Mobile_InputHandler>();
       //}
       //else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
       //{
        //    inputHandler = gameObject.AddComponent<PC_InputHandler>();
       //}
       //else
       Debug.Log("Not Supported in this device.");

       inputHandler.Initialize();
       _controller.Initialize();
    }

    private void Update() 
    {
        CheckShootInput();    
    }

    private void CheckShootInput()
    {
        if(inputHandler.GetShootPressed())
        {
            _currentGun.ShootPressed();
        }
        else
            _currentGun.ShootExit();
    }
}
