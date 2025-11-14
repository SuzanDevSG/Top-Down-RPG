using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class WaveHandler : MonoBehaviour
{
    public WaveProfile waveProfile;

    [SerializeField] private int currentWaveProfileIndex = 0;
    [SerializeField] private float WaveTime = 60f;

    public UnityEvent<int> onWaveComplete;
    public UnityEvent onLevelComplete;

    public EnemySpawnEvent ChaserSpwanSO;
    public EnemySpawnEvent BomberSpwanSO;
    public EnemySpawnEvent CreeperSpwanSO;
    public Transform EnemyParent;


    private void Start()
    {
        InvokeRepeating(nameof(WaveTimer), 1, 1);
        WaveGenerator();
    }
    private void WaveTimer()
    {
        
        WaveTime--;
        //Debug.Log(Mathf.RoundToInt(WaveTime));
        
        // check if Wave is Completed
        if(EnemyParent.childCount <= 0 || WaveTime == 0)
        {
            //  wave completed .next wave start
            onWaveComplete?.Invoke(currentWaveProfileIndex);
            currentWaveProfileIndex++;

            // is next wave available ?
            if (currentWaveProfileIndex >= waveProfile.enemySpawnCount.Count)
            {
                onLevelComplete?.Invoke();
                Debug.Log("Level Completed");
                return;
            }

            //initialized next wave
            WaveGenerator();
            WaveTime += 60f;
        }
    }
    private void WaveGenerator()
    {
        ChaserSpwanSO.RaiseSpawn(waveProfile.enemySpawnCount[currentWaveProfileIndex].chaserCount);
        BomberSpwanSO.RaiseSpawn(waveProfile.enemySpawnCount[currentWaveProfileIndex].bomberCount);
        CreeperSpwanSO.RaiseSpawn(waveProfile.enemySpawnCount[currentWaveProfileIndex].creeperCount);
    }

}

[Serializable]
public class WaveSpwanAreaProfile
{
   public Vector2 minMax;
}
