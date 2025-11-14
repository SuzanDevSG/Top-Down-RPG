using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerProfileSO playerProfileSO;
    public PlayerProfile profile;
    public PlayerSession session;

    private Rigidbody rb;
    private PlayerInputSystem playerInputSystem;


    [HideInInspector]
    [Header("Accessed By Other Scripts")]

    public Vector3 playerControl;

    private void Awake()
    {
        if (DataManager.ExistData(DataType.PlayerData))
        {
            profile = DataManager.LoadData<PlayerProfile>(DataType.PlayerData);
        }
        else if (playerProfileSO == null)
        {
            playerProfileSO = Resources.Load<PlayerProfileSO>("Player/DefaultPlayerProfile");
            profile = playerProfileSO.profile;
        }
        else
        {
            profile = playerProfileSO.profile;
        }

        session = new PlayerSession(profile);
    }
    void Start()
    {
        Application.targetFrameRate = 90;
        rb = GetComponent<Rigidbody>();

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

    }
    private void MovePlayer()
    {
        Vector3 newPosition = rb.position + playerControl * (Time.fixedDeltaTime * session.profile.maxSpeed);
        rb.MovePosition(newPosition);
    }


}
