using UnityEngine;

public class PC_InputHandler : MonoBehaviour, InputHandler
{    
    public void Initialize()
    {   
    }
    public float GetHorizontalMovement()
    {
        return Input.GetAxis("Horizontal");
    }
    public float GetVerticalMovement()
    {
        return Input.GetAxis("Vertical");
    }
    public float GetHorizontalLook()
    {
        return Input.GetAxis("Mouse X");
    }
    public float GetVerticalLook()
    {
        return Input.GetAxis("Mouse Y");
    }

    bool InputHandler.GetAlternateWalk()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    bool InputHandler.GetJump()
    {
       return Input.GetKeyDown(KeyCode.Space);
    }

    bool InputHandler.GetShoot()
    {
        throw new System.NotImplementedException();
    }
}