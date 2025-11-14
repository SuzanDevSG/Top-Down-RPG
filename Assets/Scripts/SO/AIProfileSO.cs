using System;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/NPC/AIProfile")]
public class AIProfileSO : ScriptableObject
{
    public AIProfile profile;
}
[Serializable]
public class AIProfile
{
    [Header("AI Attributes")]
    public int id;
    public int level = 1;
    public int maxHealth = 20;
    public int attackDamage = 1;
    public float moveSpeed = 2;
    public int attackRange = 1;
    public LayerMask attackMask;

    [Header("AI controller Values")]
    public float timeToCalculatePath = .5f;
    public float timeToAttack = 2f;
    public float rangeToStop = 1.5f;
}

/// <summary>
/// Level Up attributes 
/// Level
/// Health
/// Damage
/// Move Speed
/// 