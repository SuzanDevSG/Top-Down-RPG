using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[CreateAssetMenu(menuName ="Weapon/Profile")]
public class WeaponProfile : ScriptableObject
{
    public int maxAmmo;
    public int maxFireRate;
    public int maxDamage;
    public float maxRange;
    public int bulletPerShot;
    public float recoil;
}