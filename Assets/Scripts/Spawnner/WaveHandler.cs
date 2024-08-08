using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class WaveHandler : MonoBehaviour
{
    public WaveProfile waveProfile;

    [SerializeField] private int currentWaveProfileIndex;
    [SerializeField] private float WaveTime = 60f;

    public UnityEvent<int> onWaveComplete;
    public UnityEvent<int> onLevelComplete;

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
            if(currentWaveProfileIndex >= waveProfile.enemyWaveSpawn.Count)
            {
                onLevelComplete?.Invoke(currentWaveProfileIndex);
                return;
            }
            //initialized next wave
            WaveGenerator();
            WaveTime += 60f * currentWaveProfileIndex;
        }
    }
    private void WaveGenerator()
    {
        Debug.Log("wave Generated");
        ChaserSpwanSO.RaiseSpawn(waveProfile.enemyWaveSpawn[currentWaveProfileIndex].chaserCount);
        BomberSpwanSO.RaiseSpawn(waveProfile.enemyWaveSpawn[currentWaveProfileIndex].bomberCount);
        CreeperSpwanSO.RaiseSpawn(waveProfile.enemyWaveSpawn[currentWaveProfileIndex].creeperCount);

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(waveProfile.randomizer.transform.position, spwanRadius);
    //    //Gizmos.DrawWireCube(transform.position,new Vector3(spwanMinMax.x*2,2,spwanMinMax.y*2));
    //    for (int i = 0;i < spwanMinMax.Count; i++)
    //    {
    //        Gizmos.DrawWireCube(transform.position ,new Vector3( spwanMinMax[i].minMax.x*2, 2,spwanMinMax[i].minMax.y*2));
    //    }
    //}

}

[Serializable]
public class WaveSpwanAreaProfile
{
   public Vector2 minMax;
}