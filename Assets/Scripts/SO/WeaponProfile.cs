using UnityEngine;

[CreateAssetMenu(menuName ="Weapon/Profile")]
public class WeaponProfile : ScriptableObject
{
    public int defaultMaxAmmo;
    [Range(0,5)] public float defaultFireRate;

    public int defaultPower;
    public int defaultDamage;
    public float defaultRange;
    public int defaultBulletPerShot;
    public float defaultRecoil;
    public float defaultReloadTime;
}