using UnityEngine;
using System;

public class Mobile_InputHandler : MonoBehaviour, InputHandler
{
    private Mobile_Input mobile_Input;
    public void Initialize()
    {
        mobile_Input = GetComponent<Mobile_Input>();
    }
    public float GetHorizontalMovement()
    {
        return mobile_Input.Joystick.Horizontal;
    }
    public float GetVerticalMovement()
    {
        return mobile_Input.Joystick.Vertical;
    }
    public float GetHorizontalLook()
    {
        return mobile_Input.LookTouchPanel.HorizonatlTouch;
    }
    public float GetVerticalLook()
    {
       return mobile_Input.LookTouchPanel.VerticalTouch;
    }
    public bool GetAlternateWalk()
    {
        return mobile_Input.AltWalkButton.IsPressed;
    }
    public bool GetJump()
    {
        return mobile_Input.JumpButton.IsPressed;
    }

    public bool GetShootPressed()
    {
        return mobile_Input.FireButton.IsPressed;
    }

    public bool GetShootBegin()
    {
        return mobile_Input.FireButton.IsBegin;
    }

    public bool GetShootEnd()
    {
        return mobile_Input.FireButton.IsEnd;
    }
}