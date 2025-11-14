using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Bullet count
/// Rate of fire
/// find target within range
/// Rotate Player towards target
/// shoot target
/// </summary>

public class WeaponHandler : MonoBehaviour
{
    public WeaponProfileSO weaponProfile;
    public WeaponProfile profile;
    public PlayerRotation playerRotation;

    public Transform shootingPos;
    public Transform pointOfGun;
    public RaycastHit hit;
    public LayerMask EnemyMask;
    public LayerMask CollisionMask;

    public static UnityAction<Transform> OnEnemyTargeted;
    public UnityEvent OnFire;

    private bool reload;
    private Coroutine reloadCoroutine;
    [SerializeField] private float bulletCount, weaponFireRateTimer;
    public Vector3 directionWithSpread;

    private void Start()
    {
        if (DataManager.ExistData(DataType.WeaponData))
        {
            profile = DataManager.LoadData<WeaponProfile>(DataType.WeaponData);
        }
        else if (weaponProfile == null)
        {
            weaponProfile = Resources.Load<WeaponProfileSO>("Weapon/AKM");
        }
        profile = weaponProfile.weaponProfile;
        bulletCount = profile.defaultMaxAmmo;

    }
    private void Update()
    {
        bool fireRateReady = CheckFireRateTimer();
        bool bulletReady = CheckBulletReady();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, profile.defaultRange, EnemyMask);

        if (hitColliders.Length <= 0)
        {
            if (bulletCount != profile.defaultMaxAmmo && !reload)
            {
                reloadCoroutine = StartCoroutine(Reload());
            }
            return;
        }

        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            Vector3 dir = (hitCollider.GetComponent<AIController>().hitPoint.position - shootingPos.position).normalized;
            if (!CheckRayHit(dir))
            {
                continue;
            }
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
        if (bulletReady && nearestEnemy != null)
        {
            if (reload)
                CancelReload();
        }

        if (fireRateReady && bulletReady)
        {
            OnEnemyTargeted?.Invoke(nearestEnemy);

            // shoot at the enemy in range
            StartCoroutine(Shoot());
        }

    }
    private bool CheckRayHit(Vector3 dir)
    {
        Debug.DrawRay(shootingPos.position, dir * profile.defaultRange, Color.black);
        int ignorePlayerMask = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("Interact"));
        if (Physics.Raycast(shootingPos.position, dir, out hit, profile.defaultRange, ignorePlayerMask))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                return true;
            }
            return false;
        }
        else
        {
            Debug.Log("Raycast didnt hit: ");
            return false;

        }
    }
    private bool CheckBulletReady()
    {
        if (bulletCount <= 0)
        {
            if (!reload)
                reloadCoroutine = StartCoroutine(Reload());
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool CheckFireRateTimer()
    {
        // Clamp Timer value between 0 and firerate
        weaponFireRateTimer = Mathf.Clamp(weaponFireRateTimer, 0f, profile.defaultFireRate);
        if (weaponFireRateTimer > 0)
            weaponFireRateTimer -= Time.deltaTime;

        // Check Timer is equal to zero and reset timer value to firerate 
        if (weaponFireRateTimer <= 0)
        {
            return true;
        }
        return false;
    }
    private IEnumerator Shoot()
    {
        weaponFireRateTimer = profile.defaultFireRate;
        yield return null;
        bulletCount--;

        float spreadX = Random.Range(-profile.defaultRecoil, profile.defaultRecoil);
        float spreadY = Random.Range(-profile.defaultRecoil, profile.defaultRecoil);
        Vector3 spread = new(spreadX, spreadY, 0);

        directionWithSpread = pointOfGun.forward + spread;

        OnFire?.Invoke();
    }
    private IEnumerator Reload()
    {
        reload = true;
        yield return new WaitForSeconds(profile.defaultReloadTime);
        ResetReload();
        reloadCoroutine = null;
    }
    private void CancelReload()
    {
        StopCoroutine(reloadCoroutine);
        reload = false;
        reloadCoroutine = null;

    }
    private void ResetReload()
    {
        bulletCount = profile.defaultMaxAmmo;
        weaponFireRateTimer = 0;
        reload = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, profile.defaultRange);
    }
}
