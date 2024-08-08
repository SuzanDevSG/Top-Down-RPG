using UnityEngine;

public class AimCalculation : MonoBehaviour
{
    public static (bool success,Vector3 hitPoint) MouseAim(Ray ray,LayerMask groundMask)
    {
        if (Physics.Raycast(ray,out var hit, Mathf.Infinity, groundMask))
        {
            return (true, hit.point);
        }
        else
        {
            return (false,Vector3.zero);
        }
    }
}