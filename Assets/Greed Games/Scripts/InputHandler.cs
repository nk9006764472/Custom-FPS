interface InputHandler
{  
    void Initialize();
    float GetHorizontalMovement();
    float GetVerticalMovement();
    float GetHorizontalLook();
    float GetVerticalLook();
    bool GetAlternateWalk();
    bool GetJump();
    bool GetShoot();
}
