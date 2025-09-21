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
    }
    private void Update()
    {
        //// Assign Values of PlayerControl

        //Vector3 control = playerController.playerControl;

        //float magnitude = control.magnitude;

        //float localAngle = Vector3.Angle(transform.forward, control) * Mathf.Deg2Rad *
        //    Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(transform.forward, control)));

        //float localX = magnitude * Mathf.Sin(localAngle);
        //float localZ = magnitude * Mathf.Cos(localAngle);

        //// Debug.Log("value of localAngle : " + localAngle + " .. local dir : " + localX + ",,," + localZ);
        //// Animate
        //controller.SetFloat("MoveX", localX);
        //controller.SetFloat("MoveZ", localZ);


        //controller.SetFloat("Speed", currentSpeed);

        // Assign Values of PlayerControl
        Vector3 control = playerController.playerControl;

        // Ignore vertical component and bail out on tiny input
        control.y = 0f;
        if (control.sqrMagnitude < 0.0001f)
        {
            controller.SetFloat("MoveX", 0f);
            controller.SetFloat("MoveZ", 0f);
            controller.SetFloat("Speed", currentSpeed);
            return;
        }

        // Convert world-space control into local-space so diagonal movement maps correctly
        Vector3 local = transform.InverseTransformDirection(control);

        // Clamp to [-1,1] to match typical animator parameters
        float localX = Mathf.Clamp(local.x, -1f, 1f);
        float localZ = Mathf.Clamp(local.z, -1f, 1f);

        // Animate
        controller.SetFloat("MoveX", localX);
        controller.SetFloat("MoveZ", localZ);

        controller.SetFloat("Speed", currentSpeed);
    }
    private void FixedUpdate()
    {
        currentSpeed = Vector3.Distance(initialPos, transform.position) / Time.fixedDeltaTime;
        initialPos = transform.position;
    }
}
