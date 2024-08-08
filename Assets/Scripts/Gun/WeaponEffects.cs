using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    private WeaponHandler weaponHandler;

    public AudioSource audioSource;
    public AudioClip clip;

    public GameObject  bullet, muzzleFlash, stoneHitEffect, bloodHitEffect;
    private float forwardForce = 50f;

    private void Start()
    {
        weaponHandler = GetComponent<WeaponHandler>();

        weaponHandler.OnFire.AddListener(PlayShootClip);
        weaponHandler.OnFire.AddListener(InstantiateParticles);

        weaponHandler.HitEffect.AddListener(AfterHitEffect);

    }
    private void OnDestroy()
    {
        weaponHandler.OnFire.RemoveAllListeners();
    }
    void PlayShootClip()
    {
        audioSource.PlayOneShot(clip);
    }
    void InstantiateParticles()
    {
        Instantiate(muzzleFlash, weaponHandler.pointOfGun.position, weaponHandler.pointOfGun.rotation);

        //Vector3 spread = new(Random.Range(-weaponHandler.weaponStats.recoil, weaponHandler.weaponStats.recoil),
        //    Random.Range(-weaponHandler.weaponStats.recoil, weaponHandler.weaponStats.recoil),0);
        
        GameObject firedBullet = Instantiate(bullet, weaponHandler.pointOfGun.transform.position, weaponHandler.shootingPos.transform.rotation);
        firedBullet.transform.forward = weaponHandler.directionWithSpread.normalized;
        firedBullet.transform.GetComponent<Rigidbody>().AddForce(firedBullet.transform.forward * forwardForce, ForceMode.Impulse);
        Destroy(firedBullet, 2f);
    }
    void AfterHitEffect(RaycastHit hit)
    {

        if (hit.transform.CompareTag("Enemy"))
        {
            Instantiate(bloodHitEffect, hit.point, Quaternion.Euler(-hit.normal));
        }
        if (hit.transform.CompareTag("Wall"))
        {
            Instantiate(stoneHitEffect, hit.point, Quaternion.Euler(-hit.normal));
        }
    }


}
