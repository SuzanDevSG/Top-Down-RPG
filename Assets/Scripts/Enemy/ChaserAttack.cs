using UnityEngine;

public class ChaserAttack : AIController
{
    protected override void Attack()
    {
        if (!Physics.Raycast(hitPoint.position, hitPoint.forward, out hit, profile.attackRange, profile.attackMask))
        {
            return;
        }

        if (hit.collider.TryGetComponent(out PlayerStatsHandler healthPresenter))
        {
            healthPresenter.DealDamage(profile.attackDamage);
        }
        else
        {
            Debug.Log("StatsHandler Not Found in : " + gameObject.name);
        }
    }

}
