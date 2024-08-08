using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AIAnimator : MonoBehaviour
{
    [SerializeField] protected AIController controller;
    [SerializeField] protected Animator animator;

    private float speed;
    private Vector3 initialPosition;
    protected virtual void Start()
    {
        AIController controller = GetComponent<AIController>();

        initialPosition = transform.position;
        
    }
    private void Update()
    {
        speed = Vector3.Distance(initialPosition, transform.position) / Time.fixedDeltaTime;
        initialPosition = transform.position;
        MoveAnimation();
    }
    public void MoveAnimation()
    {
        animator.SetFloat("Speed", speed);
    }
    public void StartAttack()
    {
        StartAttackAnimation();
    }
    protected abstract void StartAttackAnimation();
}
