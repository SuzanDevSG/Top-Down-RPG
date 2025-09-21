using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour
{
    public AIController AIController;
    public AIProfile aiProfile;
    private bool death = false;

    [SerializeField] private float currentHealth;

    public void Start()
    {
        AIController = GetComponent<AIController>();
        currentHealth = aiProfile.maxHealth;
        Debug.Log(gameObject.name + " : " +  aiProfile.maxHealth);
    }
    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        Mathf.Clamp(currentHealth, 0, aiProfile.maxHealth);
        Debug.Log(gameObject.tag + " health : " + currentHealth);
        if (currentHealth <= 0 && !death)
        {
            AIController.onDie?.Invoke();
            death = true;
            AIController.agent.isStopped = true;
            AIController.agent.speed = 0;
            Destroy(gameObject,1f);
        }

    }
}
