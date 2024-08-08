using UnityEngine;

public class EnemyMeleeAttack : AIController
{
    [SerializeField] private Transform hitPoint;
    protected override void Attack()
    {

        
        Debug.Log("Attacking");
        
        if (!Physics.Raycast(hitPoint.position, hitPoint.forward, out hit, aiProfile.attackRange, aiProfile.attackMask))
        {
            return;
        }

        Debug.Log("Player Found by : " + gameObject.name);
        
        if (hit.collider.TryGetComponent(out PlayerStatsHandler healthPresenter))
        {
            healthPresenter.DealDamage(aiProfile.attackDamage);
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("StatsHandler Not Found in : " + gameObject.name);
        }
    }

}
