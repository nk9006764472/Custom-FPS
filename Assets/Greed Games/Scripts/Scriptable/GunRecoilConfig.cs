using UnityEngine;

[CreateAssetMenu(menuName = "Gun Recoil Config", order = 2)]
public class GunRecoilConfig : ScriptableObject
{
    public float _power;
    public float _multipler;
    public float _recoilCoolDownTime;
}
