using System.Collections;
using UnityEngine;

public abstract class CollectableDrops : MonoBehaviour, ICollectable ,IInteractable
{
    public float moveSpeed = 10f;
    private bool isCollected = false;

    public void Collect(Transform player)
    {
        if (isCollected) return;
        isCollected = true;

        // Disable collider/physics
        Collider col = GetComponent<Collider>();
        if (col) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        // Start animation
        StartCoroutine(AnimateToPlayer(player));
    }

    private IEnumerator AnimateToPlayer(Transform player)
    {
        while (Vector3.Distance(transform.position, player.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        ApplyEffect(player);
        Destroy(gameObject);
    }
    public abstract void ApplyEffect(Transform target);
    public abstract void Interact(Transform target);
}
