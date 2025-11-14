using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon/Profile")]
public class WeaponProfileSO : ScriptableObject
{
    public WeaponProfile weaponProfile;
}
[Serializable]
public class WeaponProfile
{
    public int id;
    [Range(0, 5)] public float defaultFireRate;

    // Level Up attributes
    public int level = 1;
    public int defaultMaxAmmo = 5;
    public int defaultDamage = 5;
    public float defaultRange = 20;
    public float defaultReloadTime = 2f;


    public int defaultBulletPerShot = 1;
    public int defaultPower = 30;
    public float defaultRecoil = 0;
}