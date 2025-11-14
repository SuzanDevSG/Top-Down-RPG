using UnityEngine;
public interface IInteractable
{
    void Interact(Transform target);
}
public interface ICollectable
{
    void Collect(Transform target);
    void ApplyEffect(Transform target);
}
