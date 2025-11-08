using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerProfile playerProfile;
    private Rigidbody rb;
    private PlayerInputSystem playerInputSystem;


    private float speed;
    

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


}
