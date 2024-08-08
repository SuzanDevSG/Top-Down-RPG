public class CreeperAnimation : AIAnimator
{
    protected override void StartAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

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
}
