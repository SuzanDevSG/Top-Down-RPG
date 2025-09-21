using System.Collections;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    public PlayerProfile playerProfileStatsHandler;
    private PlayerAnimation playerAnimation;
    public bool isDead;

    [SerializeField]private float currentHealth;
    private Coroutine playerDeath;
    private void Start()
    {
        if(playerProfileStatsHandler == null)
        {
            playerProfileStatsHandler = Resources.Load<PlayerProfile>("Player/DefaultPlayerProfile");
        }
        playerAnimation = GetComponent<PlayerAnimation>();
        currentHealth = playerProfileStatsHandler.maxHealth;
    }
    public void DealDamage(float damage)
    {
        currentHealth  -= damage;
        Debug.Log(transform.tag + " health : " + currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, playerProfileStatsHandler.maxHealth);
        if(currentHealth <= 0)
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