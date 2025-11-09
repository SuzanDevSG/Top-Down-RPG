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
    public WeaponProfile weaponProfile;
    public PlayerRotation playerRotation;

    public Transform shootingPos;
    public Transform pointOfGun;
    public RaycastHit hit;
    public LayerMask layerMask;

    public static UnityAction<Transform> OnEnemyTargeted;
    public UnityEvent OnFire;

    private bool reload;
    private Coroutine reloadCoroutine;
    [SerializeField] private float bulletCount, weaponFireRateTimer;
    public Vector3 directionWithSpread;

    private void Start()
    {
        if (weaponProfile == null)
        {
            weaponProfile = Resources.Load<WeaponProfile>("Weapon/AKMProfile");
        }

        bulletCount = weaponProfile.defaultMaxAmmo;

    }
    private void Update()
    {
        bool fireRateReady = CheckFireRateTimer();
        bool bulletReady = CheckBulletReady();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, weaponProfile.defaultRange, layerMask);

        if (hitColliders.Length <= 0)
        {
            if (bulletCount != weaponProfile.defaultMaxAmmo && !reload)
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
        weaponFireRateTimer = Mathf.Clamp(weaponFireRateTimer, 0f, weaponProfile.defaultFireRate);
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
        weaponFireRateTimer = weaponProfile.defaultFireRate;
        yield return null;
        bulletCount--;

        float spreadX = Random.Range(-weaponProfile.defaultRecoil, weaponProfile.defaultRecoil);
        float spreadY = Random.Range(-weaponProfile.defaultRecoil, weaponProfile.defaultRecoil);
        Vector3 spread = new(spreadX, spreadY, 0);

        directionWithSpread = pointOfGun.forward + spread;

        OnFire?.Invoke();
    }
    private IEnumerator Reload()
    {
        reload = true;
        yield return new WaitForSeconds(weaponProfile.defaultReloadTime);
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
        bulletCount = weaponProfile.defaultMaxAmmo;
        weaponFireRateTimer = 0;
        reload = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponProfile.defaultRange);
    }
}
