using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    public float maxHealth = 100;
    [SyncVar(hook = nameof(onHealthChanged))]
    public float currentHealth = 100;
    public GameObject healthCanvas;
    public GameObject healthBar;
    public Image healthFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isLocalPlayer)
        {
            healthCanvas.gameObject.SetActive(false);
        }
        else
        {
            healthCanvas.gameObject.SetActive(true);
        }
    }

    void Update()
    {


    }

    public void onHealthChanged(float oldHealth, float newHealth)
    {
        healthFill.fillAmount = newHealth / 100;
    }

    [Command(requiresAuthority = false)]
    public void CmdTakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
