using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Camera mainCam;

    private Ray ray;
    public LayerMask groundMask;
    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        RotaionWithAim();
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
