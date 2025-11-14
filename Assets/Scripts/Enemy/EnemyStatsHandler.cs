using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour
{
    public AIController AIController;
    public AIProfileSO aiProfile;
    private AIProfile profile;

    [SerializeField] private float currentHealth;

    public void Start()
    {
        if (DataManager.ExistData(DataType.EnemyData))
        {
            profile = DataManager.LoadData<AIProfile>(DataType.EnemyData);
        }
        else
        {
            if (aiProfile == null)
            {
                aiProfile = Resources.Load<AIProfileSO>("Enemy/DefaultAIProfile");

            }
            else
                profile = aiProfile.profile;
        }
        AIController = GetComponent<AIController>();
        currentHealth = profile.maxHealth;
    }
    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        Mathf.Clamp(currentHealth, 0, profile.maxHealth);
        if (currentHealth <= 0)
        {
            AIController.RaiseOnDie();
        }

    }
}
