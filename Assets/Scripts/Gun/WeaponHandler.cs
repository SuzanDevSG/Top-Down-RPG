using System;
using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// Input
/// Bullet count
/// Rate of fire
/// Shoot
/// Audio and Particles
/// HitInfo and DealDamage
/// </summary>
public class WeaponHandler : MonoBehaviour
{
    public PlayerController playerController;
    public WeaponProfile weaponProfile;



    public Transform shootingPos;
    public Transform pointOfGun;
    public RaycastHit hit;
    public LayerMask layerMask;

    public UnityEvent OnFire;
    public UnityEvent<RaycastHit> HitEffect;

    public static bool fire;
    private bool reload;
    [SerializeField] private float bulletCount, timer;
    public Vector3 directionWithSpread;

    void Start()
    {
        weaponProfile = Resources.Load<WeaponProfile>("Weapon/AKMProfile");

        bulletCount = weaponProfile.maxAmmo;
    }

    void Update()
    {
        GetInput();
        CheckShootReady(Shoot);
    }

    private void Reload()
    {
        Invoke(nameof(ResetReload), 1.5f);
    }
    private void ResetReload()
    {
        bulletCount = weaponProfile.maxAmmo;
        reload = false;
    }

    private void GetInput()
    {
        // Fire using Mouse left Button click
        fire = Input.GetMouseButton(0);

        // Reload to get ammo
        reload = Input.GetKeyDown(KeyCode.R);

        if(reload)  Reload();
    }

    private void CheckShootReady(Action callBack)
    {
        // limiting timer to 0 and FireRateTime
        timer = Mathf.Clamp(timer, 0, weaponProfile.maxFireRate);

        if (timer > 0)
            timer -= Time.deltaTime;
        if (!fire)
            return;
        if (bulletCount <= 0)
            return;


        if(playerController.ControlLegacy && playerController.playerControl != Vector3.zero) 
        {
                return;
        }


        // Check Timer is equal to zero and reset timer value to firerate 
        if (timer == 0)
        {
            timer = 1f / weaponProfile.maxFireRate;
            for (int i = 0; i < weaponProfile.bulletPerShot; i++)
            {
            callBack?.Invoke();
            }

        }
    }


    private void Shoot()
    {
        bulletCount--;

        //Debug.Log("Gun Shot");


        float spreadX = UnityEngine.Random.Range(-weaponProfile.recoil, weaponProfile.recoil);
        float spreadY = UnityEngine.Random.Range(-weaponProfile.recoil, weaponProfile .recoil);
        Vector3 spread = new(spreadX, spreadY, 0);

        directionWithSpread = shootingPos.forward + spread ;
        
        // Debug.Log(spread);

        OnFire.Invoke();
        if (!Physics.Raycast(shootingPos.position, directionWithSpread, out hit, weaponProfile.maxRange, layerMask))
        {
            Debug.Log("Couldnot hit object");;
            return;
        }
        HitEffect.Invoke(hit);

        if (hit.transform.gameObject.TryGetComponent<EnemyStatsHandler>(out EnemyStatsHandler statHandler))
        {
            statHandler.DealDamage(weaponProfile.maxDamage);
        }




    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(shootingPos.position , shootingPos.forward * weaponStats.maxRange);

        
    }*/
}
