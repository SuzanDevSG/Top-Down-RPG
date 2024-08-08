using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        isDead = true;
        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);
        StopDie();

    }

    private void StopDie()
    {
        StopCoroutine(playerDeath);

    }

}
