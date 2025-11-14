using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave/WaveProfile")]
public class WaveProfile : ScriptableObject
{
    public List<EnemySpawnCount> enemySpawnCount;
}

/// <summary>
/// How many enemies to spawn
/// </summary>

[Serializable]
public class EnemySpawnCount
{

    public int chaserCount;
    public int bomberCount;
    public int creeperCount;
}

