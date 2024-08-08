public class BomberAnimations : AIAnimator
{
    protected override void StartAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}