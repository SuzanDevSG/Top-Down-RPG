using System;
using UnityEngine;


[CreateAssetMenu(menuName ="SO/NPC/AIProfile")]
public class AIProfile : ScriptableObject
{
    [Header("AI Attributes")]
    public int id;
    public float maxHealth;
    public int attackDamage;
    public int attackRange;

    [Header("AI controller Values")]
    public float timeToCalculatePath;
    public float timeToAttack;
    public Vector2 rangeToStop;
    public LayerMask attackMask;
}


