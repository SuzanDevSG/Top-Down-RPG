using UnityEngine;

public class EnemyBomber : AIController
{
    public GameObject bomb;

    [SerializeField] private Transform spwanPoint;
    [SerializeField] private float upwardForce;
    [SerializeField] private float forwardForce;

    protected override void Attack()
    {
        GameObject spwanedBomb  = Instantiate(bomb,spwanPoint.position,spwanPoint.rotation);

        if(spwanedBomb.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
            rb.AddForce(transform.up * upwardForce, ForceMode.Impulse);
        }

        
        
    }
        //Instantiate(spwanPoint,hit.point,Quaternion.identity);
}
