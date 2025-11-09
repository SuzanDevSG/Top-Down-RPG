using System;
using System.Collections;
using UnityEngine;

public enum RotationType
{
    Normal,
    TowardsEnemy
}

public class PlayerRotation : MonoBehaviour
{
    private PlayerController playerController;
    private float lookSpeed;
    private float currentVelocity = 1;
    private Transform nearestEnemy = null;

    public RotationType rotationType = RotationType.Normal;
    private Coroutine resetRotation;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        lookSpeed = playerController.playerProfile.maxLookSpeed;

        WeaponHandler.OnEnemyTargeted += SetNearestEnemy;
    }
    private void OnDestroy()
    {
        WeaponHandler.OnEnemyTargeted -= SetNearestEnemy;
    }

    private void RotatePlayer(Vector3 inputControl)
    {
        if (inputControl.sqrMagnitude <= 0f)
        {
            return;
        }
        // calculate angle based on input vector
        var direction = Mathf.Atan2(inputControl.x, inputControl.z) * Mathf.Rad2Deg;
        // smoothly damp the angle
        var angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, direction, ref currentVelocity, lookSpeed);
        // rotate player using the angle
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void LateUpdate()
    {
        switch (rotationType)
        {
            case RotationType.Normal:
                // live input vector from PlayerController
                Vector3 liveControl = playerController != null ? playerController.playerControl : Vector3.zero;

                // normal rotation based on input
                RotatePlayer(liveControl);
                nearestEnemy = null;
                break;
            case RotationType.TowardsEnemy:

                RotateTowardsEnemy(nearestEnemy);

                break;
        }


    }
    private Transform RotateTowardsEnemy(Transform nearestEnemy)
    {
        Vector3 targetDir = (nearestEnemy.position - transform.position).normalized;
        targetDir.y = 0f;
        if (targetDir.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        return nearestEnemy;
    }
    private void SetNearestEnemy(Transform nearestEnemy)
    {
        this.nearestEnemy = nearestEnemy;
        rotationType = RotationType.TowardsEnemy;

        if (resetRotation != null)
            StopCoroutine(resetRotation);
        resetRotation = StartCoroutine(ResetRotationType());
    }
    private IEnumerator ResetRotationType()
    {
        yield return new WaitForSeconds(0.5f);
        rotationType = RotationType.Normal;
    }   

}
