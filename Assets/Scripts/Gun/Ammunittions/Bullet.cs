using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    LayerMask enemyMask;
    private Action<Transform> AfterHitEffects;

    public void SetBulletProperties(int damage, LayerMask enemyMask,Action<Transform> AfterHitEffects)
    {
        this.damage = damage;
        this.enemyMask = enemyMask;
        this.AfterHitEffects = AfterHitEffects;
    }   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) return;

        if((enemyMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if (other.TryGetComponent<EnemyStatsHandler>(out EnemyStatsHandler statHandler))
            {
                statHandler.DealDamage(damage);
            }
        }
        
        if (!other.CompareTag("Player"))
        {
            AfterHitEffects?.Invoke(other.transform);
            Destroy(gameObject);
        }

    }

}

