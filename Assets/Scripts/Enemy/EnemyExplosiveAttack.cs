using UnityEngine;

public class EnemyExplosiveAttack : AIController
{
    [SerializeField] private GameObject explosion;
    // private bool playerHit = false;
    protected override void Attack()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, aiProfile.attackRange, aiProfile.attackMask);
        foreach (Collider collider in hitColliders)
        {
            Debug.Log(collider.name);
            if(collider.transform.TryGetComponent<PlayerStatsHandler>(out PlayerStatsHandler playerStatsHandler))
                playerStatsHandler.DealDamage(aiProfile.maxHealth);

            if(collider.transform.TryGetComponent<EnemyStatsHandler>(out EnemyStatsHandler enemyStatsHandler))
            {
                enemyStatsHandler.DealDamage(aiProfile.maxHealth);
            }
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (playerHit) Gizmos.DrawSphere(transform.position, aiProfile.attackRange);

    }*/

}