using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerProfile playerProfile;
    private Rigidbody rb;
    private PlayerInputSystem playerInputSystem;


    private float speed;
    private float lookSpeed;
    private float currentVelocity = 1;

    [Header("Accessed By Other Scripts")]
    public Vector3 playerControl;

    void Start()
    {
        Application.targetFrameRate = 90;
        rb = GetComponent<Rigidbody>();
        if (playerProfile == null)
        {
            playerProfile = Resources.Load<PlayerProfile>("Player/DefaultPlayerProfile");
        }

        speed = playerProfile.maxSpeed;
        lookSpeed = playerProfile.maxLookSpeed;

        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Player.Enable();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        //RotatePlayer();
    }

    public void GetInput()
    {
        Vector2 control = playerInputSystem.Player.Movement.ReadValue<Vector2>();
        playerControl = new Vector3(control.x, 0f, control.y);

        //if (playerControl.sqrMagnitude > 1f)
        //{
        //    playerControl.Normalize();
        //}
    }

    private void MovePlayer()
    {
        Vector3 newPosition = rb.position + playerControl * (Time.fixedDeltaTime * speed);
        rb.MovePosition(newPosition);
    }

    private void RotatePlayer()
    {
        if (playerControl.sqrMagnitude <= 0f)
        {
            return;
        }

        // input taken to rotate
        var direction = Mathf.Atan2(playerControl.x, playerControl.z) * Mathf.Rad2Deg;
        // Amount of Angle to rotate
        var angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, direction, ref currentVelocity, lookSpeed);
        // rotate player using the angle
        rb.rotation = Quaternion.Euler(0, angle, 0);
        //transform.rotation = Quaternion.Euler(0, angle, 0);
        // Debug.Log(playerControl.magnitude);
    }
}
