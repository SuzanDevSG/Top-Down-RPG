using Cinemachine.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


[CreateAssetMenu(menuName = "Wave/WaveProfile")]
public class WaveProfile : ScriptableObject
{
    public Spawnner spawn;
    public List<EnemyWaveSpawn> enemyWaveSpawn;
}

/// <summary>
/// How many enemies to spawn
/// </summary>

[Serializable]
public class EnemyWaveSpawn
{

    public int chaserCount;
    public int bomberCount;
    public int creeperCount;
}

