using UnityEngine;

[CreateAssetMenu(menuName = "Gun Recoil Config", order = 2)]
public class GunRecoilConfig : ScriptableObject
{
    public float _recoilCoolDownTime;

    
    [Header("Vertical Recoil")]
    public float _power;
    public float _multipler;
    public float _maxAngleDeviationY;

    [Header("Horizontal Recoil")]
    public float _maxAngleDeviationX;
    public float _angleDeviationX;

}
