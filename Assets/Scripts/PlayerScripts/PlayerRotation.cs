using UnityEngine;
using UnityEngine.Events;

public class PlayerRotation : MonoBehaviour
{
    private PlayerController playerController;
    private float lookSpeed;
    private float currentVelocity = 1;

    [SerializeField] private float AttackRadius = 5f;
    [SerializeField] private LayerMask EnemyLayer;

    public UnityEvent OnEnemyInRange;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        lookSpeed = playerController.playerProfile.maxLookSpeed;
    }

    private void RotatePlayer(Vector3 inputControl)
    {
        if (inputControl.sqrMagnitude <= 0f)
        {
            return;
        }

        // input taken to rotate
        var direction = Mathf.Atan2(inputControl.x, inputControl.z) * Mathf.Rad2Deg;
        // Amount of Angle to rotate
        var angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, direction, ref currentVelocity, lookSpeed);
        // rotate player using the angle
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void Update()
    {
        // Always read the live input vector from PlayerController
        Vector3 liveControl = playerController != null ? playerController.playerControl : Vector3.zero;

        // First, handle normal rotation based on input
        RotatePlayer(liveControl);

        // Then check for enemies in range and override rotation if an enemy is found
        Collider[] hitColliders = Physics.OverlapBox(transform.position,
            new Vector3(AttackRadius, 5, AttackRadius), transform.rotation, EnemyLayer);

        if (hitColliders.Length <= 0)
        {
            return;
        }

        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = hitCollider.transform;
            }
        }

        if (nearestEnemy == null)
        {
            return;
        }

        // Face the nearest enemy 
        Vector3 targetDir = (nearestEnemy.position - transform.position);
        targetDir.y = 0f;
        if (targetDir.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        // invoke the event while enemy is in range (fires each frame enemy remains in range)
        OnEnemyInRange?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(AttackRadius * 2, 5, AttackRadius * 2));
    }
}
