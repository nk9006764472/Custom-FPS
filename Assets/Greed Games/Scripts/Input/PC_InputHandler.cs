using System;
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

    public bool GetAlternateWalk()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool GetJump()
    {
       return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetShootPressed()
    {
        return Input.GetMouseButton(0);
    }

    public bool GetShootBegin()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool GetShootEnd()
    {
        return Input.GetMouseButtonUp(0);
    }
}