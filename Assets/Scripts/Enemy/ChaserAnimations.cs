using Unity.VisualScripting;
using UnityEngine;

public class ChaserAnimations : AIAnimator
{
    protected override void Start()
    {
        controller.onDie?.AddListener(DieAnimation);
    }
    private void OnDestroy()
    {
        controller.onDie?.RemoveAllListeners();

    }
    public void DieAnimation()
    {
        animator.Play("Death");
    }
    protected override void StartAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
