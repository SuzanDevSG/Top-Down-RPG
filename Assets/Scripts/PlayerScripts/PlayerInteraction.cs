using UnityEngine;

public class PlayerInteraction : PlayerController
{
    private void Update()
    {
        LayerMask InteractMask = LayerMask.GetMask("Interact");

        Collider[] hitColliders = new Collider[20];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, session.profile.interactionRadius, hitColliders, InteractMask);
        for (int i = 0; i < numColliders; i++)
        {
            hitColliders[i].TryGetComponent<IInteractable>(out IInteractable interactable);
            interactable?.Interact(transform);
        }
    }
}