using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private Ray ray;
    public LayerMask CrosshairMask;

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var (success, hitPoint) = AimCalculation.MouseAim(ray, CrosshairMask);
        if (!success)
        {
            return;
        }
        transform.position = hitPoint;
    }

}