using UnityEngine;

public class Mobile_Input : MonoBehaviour
{
    public Joystick Joystick => _joystick;
    public FireButton FireButton => _fireButton;
    public JumpButton JumpButton => _jumpButton;
    public AltWalkButton AltWalkButton => _altwalkButton;
    public LookTouchPanel LookTouchPanel => _lookTouchPanel;

    [SerializeField] private Joystick _joystick;
    [SerializeField] private FireButton _fireButton;
    [SerializeField] private JumpButton _jumpButton;
    [SerializeField] private AltWalkButton _altwalkButton;
    [SerializeField] private LookTouchPanel _lookTouchPanel;
}
