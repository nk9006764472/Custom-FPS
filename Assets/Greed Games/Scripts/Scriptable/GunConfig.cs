using UnityEngine;

[CreateAssetMenu(menuName = "Gun Config", order = 0)]
public class GunConfig : ScriptableObject
{
    public string gunName;
    public GunFireType fireType;
    public GunClass gunClass;
    public float power;
    public float fireRate;
    public float reloadTime;
    public int bulletCapacity;
    public float firstShotSpread;
    public Vector3 gunOffset;
}

public enum GunClass
{
    PISTOL,
    SHOTGUN,
    ASSAULT_RIFLE,
    SNIPER_RIFLE,
    MARKSMAN_RIFLE,
    MACHINE_GUN
}

public enum GunFireType
{
    AUTO,
    SINGLE,
    BURST
}
