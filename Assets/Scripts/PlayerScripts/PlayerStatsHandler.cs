using System.Collections;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerAnimation playerAnimation;
    public bool isDead;

    [SerializeField] private float currentHealth;
    private Coroutine playerDeath;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    private void Start()
    {
        currentHealth = playerController.session.profile.maxHealth;
        UIManager.Instance.UpdateHealthUI(currentHealth, playerController.session.profile.maxHealth);

        UIManager.Instance.SetExpUI(playerController.session.experiencePoints, playerController.session.maxExperiencePoints, playerController.session.LevelProgress);
    }
    public void UpdateExpPoints(float exp)
    {
        playerController.session.experiencePoints += exp;
        UIManager.Instance.UpdateExpUI(playerController.session.experiencePoints);

        if (playerController.session.experiencePoints >= playerController.session.maxExperiencePoints)
        {
            playerController.session.experiencePoints %= playerController.session.maxExperiencePoints;
            playerController.session.LevelProgress += 1;
            playerController.session.maxExperiencePoints *= 1.2f;
            UIManager.Instance.SetExpUI(playerController.session.experiencePoints,playerController.session.maxExperiencePoints, playerController.session.LevelProgress);
        }
    }
    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        UIManager.Instance.UpdateHealthUI(currentHealth, playerController.session.profile.maxHealth);

        currentHealth = Mathf.Clamp(currentHealth, 0, playerController.session.profile.maxHealth);
        if (currentHealth <= 0)
        {
            playerDeath = StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        playerAnimation.controller.Play("PlayerDeath");
        // disable player Control
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        playerController.enabled = false;
        

        isDead = true;
        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);
        StopCoroutine(playerDeath);
        playerController.enabled = true;
    }
}