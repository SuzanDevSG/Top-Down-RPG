using UnityEngine;

public class Bomb : MonoBehaviour 
{
    public GameObject bombEffects;
    [SerializeField] private float bombDamage;
    [SerializeField] private float bombRange;

    private Vector3 pos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            return;
        }

        Instantiate(bombEffects, transform.position, Quaternion.identity);

        Collider[] area = Physics.OverlapSphere(transform.position, bombRange);
        pos = transform.position;
        foreach (Collider colliders in area)
        {

            if (colliders.transform.TryGetComponent<PlayerStatsHandler>(out PlayerStatsHandler playerStatsHandler))
            {
                Debug.Log("Player damaged",playerStatsHandler.gameObject);
                playerStatsHandler.DealDamage(bombDamage);
            }
            if (colliders.transform.TryGetComponent<EnemyStatsHandler>(out EnemyStatsHandler enemyStatsHandler))
            {
                Debug.Log("enemy damaged",enemyStatsHandler.gameObject);
                enemyStatsHandler.DealDamage(bombDamage);
            }
        }
        //gameObject.SetActive(false);
        Destroy(gameObject,.5f);


    }

}

