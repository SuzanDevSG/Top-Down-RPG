using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Camera mainCam;
    private PlayerController playerController;

    private Ray ray;
    public LayerMask groundMask;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        if(playerController.ControlLegacy)
        {
            if (playerController.playerControl.sqrMagnitude <= 0)
            {
                RotaionWithAim();
            }
            return;
        }
        else
        {
            RotaionWithAim();
        }
    }
    private void RotaionWithAim()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition);

        var (success,hitPoint) = AimCalculation.MouseAim(ray,groundMask);
        if (!success)
        {
            return;
        }

        //transform.LookAt();

        Vector3 direction = hitPoint - transform.position;
        direction.y = 0f;
        transform.forward = direction.normalized;
    }
}
