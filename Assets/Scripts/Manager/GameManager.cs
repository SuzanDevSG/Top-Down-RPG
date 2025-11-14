using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerProfileSO playerProfileSO;
    public PlayerProfile profile;

    private void Start()
    {
        if (DataManager.ExistData(DataType.PlayerData))
        {
            profile = DataManager.LoadData<PlayerProfile>(DataType.PlayerData);
        }
        else if (playerProfileSO == null)
        {
            playerProfileSO = Resources.Load<PlayerProfileSO>("Player/DefaultPlayerProfile");
        }

        profile = playerProfileSO.profile;
    }


}