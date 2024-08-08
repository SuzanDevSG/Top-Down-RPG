using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerProfile playerProfileController;

    private Rigidbody rb;
    private float walkSpeed;
    private float speed;
    private float lookSpeed;
    private float lookWalkSpeed;
    private float currentVelocity = 1;

    [Header("Accessed By Other Scripts")]
    public GameObject weapon;
    public Vector3 playerControl;
    public bool walk;
    public bool weaponState;
    public bool ControlLegacy;

    void Start()
    {
        if(playerProfileController == null)
        {
            playerProfileController = Resources.Load<PlayerProfile>("Player/DefaultPlayerProfile");
        }  
        rb = GetComponent<Rigidbody>();
        walkSpeed = playerProfileController.maxSpeed / 2;
        lookWalkSpeed = playerProfileController.maxLookSpeed * 1.5f;

        weaponState = weapon.activeSelf;
    }

    void Update()
    {
        GetInput();
        CheckPlayerState();
    }
    private void FixedUpdate()
    {
        MovePlayer();
        //RotatePlayer();
        
    }
    public void GetInput() 
    {
        walk = Input.GetKey(KeyCode.LeftShift);
        playerControl = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if(playerControl.magnitude > 1f)
        {
            playerControl.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weapon.SetActive(!weaponState);
        }
    }
    private void CheckPlayerState()
    {
        weaponState = weapon.activeSelf;

        speed = walk ? walkSpeed : playerProfileController.maxSpeed;
        lookSpeed = walk ? lookWalkSpeed : playerProfileController.maxLookSpeed;
    }

    private void MovePlayer()
    {
        rb.MovePosition(transform.position +  playerControl * ( Time.fixedDeltaTime * speed));
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
        // Debug.Log(playerControl.magnitude);
    }
}
