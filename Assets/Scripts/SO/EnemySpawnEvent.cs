using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="SO/EnemySpwanEvent")]
public class EnemySpawnEvent : ScriptableObject
{
    public AIController EnemyAI;
    private UnityAction<int> SpawnEvent; 

    public void SubscribeSpawn(UnityAction<int> action)
    {
        SpawnEvent += action;
    }
    public void UnSubscribeSpawn(UnityAction<int> action)
    {
        SpawnEvent -= action;
    }

    public void RaiseSpawn(int count)
    {
        SpawnEvent?.Invoke(count);
    }

}