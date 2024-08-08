using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider Collision)
    {
        //Debug.Log(Collision.tag);
        if (!Collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

}

