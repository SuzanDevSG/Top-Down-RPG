using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController playerController;
    public Animator controller;

    private float currentSpeed;
    private Vector3 initialPos;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        initialPos = transform.position;

        Application.targetFrameRate = 90;
    }
    private void Update()
    {
        // Assign Values of PlayerControl

        Vector3 control = playerController.playerControl;

        float magnitude = control.magnitude;

        float localAngle = Vector3.Angle(transform.forward, control) * Mathf.Deg2Rad *
            Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(transform.forward, control)));



        float localX = magnitude * Mathf.Sin(localAngle);
        float localZ = magnitude * Mathf.Cos(localAngle);

        // Debug.Log("value of localAngle : " + localAngle + " .. local dir : " + localX + ",,," + localZ);
        // Animate
        controller.SetFloat("MoveX", localX);
        controller.SetFloat("MoveZ", localZ);


        controller.SetBool("GunActiveState", playerController.weaponState);
        // controller.SetBool("ControlType", playerController.ControlLegacy);

/*        if (playerController.walk)
        {
            controller.SetFloat("Speed", currentSpeed / 2);
        }*/
        controller.SetFloat("Speed", currentSpeed);
    }
    private void FixedUpdate()
    {
        currentSpeed = Vector3.Distance(initialPos, transform.position) / Time.fixedDeltaTime;
        initialPos = transform.position;
    }
}
