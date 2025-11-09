using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    private WeaponHandler weaponHandler;

    public AudioSource audioSource;
    public AudioClip clip;

    public GameObject bullet, muzzleFlash, stoneHitEffect, bloodHitEffect;

    private void Start()
    {
        weaponHandler = GetComponent<WeaponHandler>();

        weaponHandler.OnFire.AddListener(InstantiateParticles);
    }
    private void OnDestroy()
    {
        weaponHandler.OnFire.RemoveListener(InstantiateParticles);
    }
    void PlayShootClip()
    {
        audioSource.PlayOneShot(clip);
    }
    void InstantiateParticles()
    {
        PlayShootClip();

        Instantiate(muzzleFlash, weaponHandler.pointOfGun.position, weaponHandler.pointOfGun.rotation);

        //bullet instantiation and force application
        float bulletSpeed = weaponHandler.weaponProfile.defaultPower;

        GameObject firedBullet = Instantiate(bullet, weaponHandler.pointOfGun.transform.position, weaponHandler.shootingPos.rotation);
        var rb = firedBullet.transform.GetComponent<Rigidbody>();
        rb.velocity = weaponHandler.shootingPos.forward * bulletSpeed;
        firedBullet.transform.GetComponent<Bullet>().SetBulletProperties(weaponHandler.weaponProfile.defaultDamage, weaponHandler.layerMask, AfterHitEffect);
        Destroy(firedBullet, 2f);
    }
    public void AfterHitEffect(Transform hitTransform)
    {

        if (hitTransform.transform.CompareTag("Enemy"))
        {
            Instantiate(bloodHitEffect, hitTransform.position, Quaternion.Euler(-hitTransform.forward));
        }
        if (hitTransform.transform.CompareTag("Wall"))
        {
            Instantiate(stoneHitEffect, hitTransform.position, Quaternion.Euler(-hitTransform.forward));
        }
    }


}
