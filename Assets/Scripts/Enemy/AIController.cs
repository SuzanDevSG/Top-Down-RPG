using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public abstract class AIController : MonoBehaviour
{
    public AIProfile aiProfile;
    public NavMeshAgent agent;
    [SerializeField] private AIAnimator aiAnimator;
    private float currentDistance;

    public UnityEvent onMove;
    public UnityEvent onAttack;
    public UnityEvent onDie;

    private float currentTimeToCalculatePath; //time check before getting next path
    private float currentTimeToAttack ; // time check before attacking
    private NavMeshPath currentPath; // received calculated path
    private Transform target; //player
    protected RaycastHit hit; //raycast hit damage
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPath = new NavMeshPath(); //initialize
        currentTimeToCalculatePath = aiProfile.timeToCalculatePath;  //2s 2s
        currentTimeToAttack = aiProfile.timeToAttack; //2s 2s
        target = GameObject.FindGameObjectWithTag("Player").transform; //target
        InitializeStats();
    }
    private void OnDisable()
    {
        onDie?.RemoveAllListeners();
    }

    //[SerializeField] private Animator anim;
    private void InitializeStats()
    {
        //x=2 y=3 2,3
        agent.stoppingDistance = Random.Range(aiProfile.rangeToStop.x, aiProfile.rangeToStop.y);
    }
    protected virtual void Update()
    {
        if (target.GetComponent<PlayerStatsHandler>().isDead) 
        {
            currentTimeToAttack = 0;
            return;
        }
        if (IsReadyToAttack()) //return type of boolean function
        {
            
            agent.isStopped = true;
            Vector3 angle  = new(target.position.x, transform.position.y - 0.08f, target.position.z);
            transform.LookAt(angle);
            ReadyToAttack();

            
        }
        else
        {
            agent.isStopped = false;
            MoveToPath();
            //aiAnimator.StopAttackAnimation();
        }
    }


    #region Move

    private void MoveToPath()
    {
        CalculatePath(); 
        DrawPath(); //debug
        Move();
    }

    private void CalculatePath()
    {
        if (currentTimeToCalculatePath < aiProfile.timeToCalculatePath)
        {
            currentTimeToCalculatePath += Time.deltaTime;
            return;
        }
        currentTimeToCalculatePath -= aiProfile.timeToCalculatePath;
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, currentPath);
    }

    private void DrawPath()
    {
        for (int i = 0; i < currentPath.corners.Length - 1; i++)
        {
            Debug.DrawLine(currentPath.corners[i], currentPath.corners[i + 1], Color.red);
        }
    }

    private void Move()
    {
        if (currentPath == null)
        {
            return;
        }
        if (currentPath.corners.Length <= 0)
        {
            return;
        }
        agent.SetDestination(currentPath.corners[^1]); //array -> last
        onMove?.Invoke();
    }

    #endregion

    #region Attack
    private void ReadyToAttack()
    {
        
        currentTimeToAttack += Time.deltaTime;
        if (currentTimeToAttack < aiProfile.timeToAttack)
        {
            return;
        }
        aiAnimator.StartAttack();
        onAttack?.Invoke();
        Attack();
        currentTimeToAttack = 0;
    }
    private bool IsReadyToAttack()
    {
        if(target == null)
        {
            return false;
        }
        currentDistance = Vector3.Distance(target.position, transform.position); //distance get
        return currentDistance <= agent.stoppingDistance; //distance check
    }
    protected abstract void Attack();
    #endregion

}
