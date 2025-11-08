using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class Spawnner : MonoBehaviour
{
    public EnemySpawnEvent EnemySpwanSO;
    public Transform EnemyParent;
    public float radius = 5;
    private void Start()
    {
        EnemySpwanSO.SubscribeSpawn(SpawnEnemies);
    }
    private void OnDestroy()
    {
        EnemySpwanSO.UnSubscribeSpawn(SpawnEnemies);
    }
    public Vector3 GetSpawnPoint() //x=>10 -10,10
    {
        var spawnPoint = transform.position + Random.insideUnitSphere * radius;
        spawnPoint.y = 0;
        Debug.Log(spawnPoint);
        return spawnPoint;
    }

    public Vector3 GetDonutSpawnPoint()
    {
        /// <summary>
        /// Get a random point in a donut shape (torus) around the spawner.
        /// </summary>
        /// <returns>A random point in the donut shape.</returns>

        Vector3 spawnPoint = transform.position + Random.insideUnitSphere * radius;
        spawnPoint.y = 0;

        float donutRadius = radius * 0.5f;
        spawnPoint += Random.insideUnitSphere * donutRadius;

        return spawnPoint;
    }
    public void SpawnEnemies(int count)
    {
        Debug.Log("Call for Enemy Generating");
        for (var j = 0; j < count; j++)
        {
            var tempSpawn = Instantiate(EnemySpwanSO.EnemyAI, GetSpawnPoint(), Quaternion.identity, EnemyParent);
            tempSpawn.gameObject.SetActive(true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

/*public static Vector3 GetSpawnPoint(Vector2 minMax, int spawnAreaRadius) //x=>10 -10,10
{

    //var spwanPoint = Random.RandomRange(new Vector3(0, 0, 0), new Vector3(100, 0, 100));

    /// <summary>
    /// random Vector3 Point around the transform.
    /// </summary>
    var spawnPoint = Random.insideUnitCircle * spawnAreaRadius; //gets random spawn point inside a sphere
     //sets y to 0 to make it so that enemy is not spawned in air
    spawnPoint.x = Mathf.Clamp(spawnPoint.x, -minMax.x, minMax.x); //clamped x value to minmax x value
    spawnPoint.y = Mathf.Clamp(spawnPoint.y, -minMax.y, minMax.y); //clamped z value to minmax y value
    return spawnPoint;
}*/
