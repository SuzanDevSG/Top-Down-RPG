using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public abstract class AIController : MonoBehaviour
{
    public AIProfileSO aiProfile;
    public AIProfile profile;
    public NavMeshAgent agent;
    [SerializeField] private AIAnimator aiAnimator;
    private float currentDistance;
    public Transform hitPoint;

    public UnityEvent onMove;
    public UnityEvent onAttack;
    public UnityEvent onDie;

    private float currentTimeToCalculatePath; //time check before getting next path
    private float currentTimeToAttack; // time check before attacking
    private NavMeshPath currentPath; // received calculated path
    private Transform target; //player
    protected RaycastHit hit; //raycast hit damage

    private Coroutine dieCoroutine;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform; //target
    }
    private void Start()
    {
        if (DataManager.ExistData(DataType.EnemyData))
        {
            profile = DataManager.LoadData<AIProfile>(DataType.EnemyData);
        }
        else
        {
            if (aiProfile == null)
            {
                aiProfile = Resources.Load<AIProfileSO>("Enemy/DefaultAIProfile");
            }
            profile = aiProfile.profile;
        }
        SetAIAttributes();

        onDie.AddListener(DieEffects);
    }
    private void SetAIAttributes()
    {
        currentPath = new NavMeshPath(); //initialize
        currentTimeToCalculatePath = profile.timeToCalculatePath;
        currentTimeToAttack = profile.timeToAttack;
        agent.speed = profile.moveSpeed;
        agent.stoppingDistance = profile.rangeToStop;

    }
    private void OnDestroy()
    {
        onDie.RemoveListener(DieEffects);
    }
    public void RaiseOnDie()
    {
        onDie?.Invoke();
    }
    void DieEffects()
    {
        // disable ai
        GetComponent<Collider>().enabled = false;
        agent.speed = 0;
        dieCoroutine = StartCoroutine(DieEffectsAfterAnimation());
        enabled = false;
    }
    private IEnumerator DieEffectsAfterAnimation()
    {
        // wait for animation
        yield return new WaitForSeconds(1f);
        DropsManager.Instance.SpawnExpDrop(transform.position);
        Destroy(gameObject);
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
            Vector3 angle = new(target.position.x, transform.position.y - 0.08f, target.position.z);
            transform.LookAt(angle);
            ReadyToAttack();
        }
        else
        {
            agent.isStopped = false;
            MoveToPath();
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
        if (currentTimeToCalculatePath < profile.timeToCalculatePath)
        {
            currentTimeToCalculatePath += Time.deltaTime;
            return;
        }
        currentTimeToCalculatePath -= profile.timeToCalculatePath;
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
        if (currentTimeToAttack < profile.timeToAttack)
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
        if (target == null)
        {
            return false;
        }
        currentDistance = Vector3.Distance(target.position, transform.position); //distance get
        return currentDistance <= agent.stoppingDistance; //distance check
    }
    protected abstract void Attack();
    #endregion

}
