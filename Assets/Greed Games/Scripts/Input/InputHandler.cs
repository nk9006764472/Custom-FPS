interface InputHandler
{  
    void Initialize();
    float GetHorizontalMovement();
    float GetVerticalMovement();
    float GetHorizontalLook();
    float GetVerticalLook();
    bool GetAlternateWalk();
    bool GetJump();
    bool GetShootPressed();
    bool GetShootBegin();
    bool GetShootEnd();
    bool GetGunDrop();
}
