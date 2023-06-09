using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthText;

    [Header("Armor")]
    [SerializeField] private Image armorBar;
    [SerializeField] private Text armorText;

    [Header("Debug")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] private float maxArmor;
    [SerializeField] private float currentArmor;



    public void Init(float startHealth, float maxArmor, float startArmor)
    {
        maxHealth = startHealth;

        currentHealth = maxHealth;

        this.maxArmor = maxArmor;
        currentArmor = startArmor;

        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.DOFillAmount(currentHealth / maxHealth, 0.3f).Play();
        armorBar.DOFillAmount(currentArmor / maxArmor, 0.3f).Play();
        healthText.text = currentHealth.ToString() + '/' + maxHealth.ToString();
        armorText.text = currentArmor.ToString() + '/' + maxArmor.ToString(); ;
    }

    public bool TakeDamage(float damage)
    {
        if (currentArmor > damage)
        {
            currentArmor -= damage;
            damage = 0;
        } else
        {
            damage -= currentArmor;
            currentArmor = 0;
        }

        currentHealth -= damage;

        UpdateUI();

        bool isDead = currentHealth <= 0;
        return isDead;
    }

    public void AddHP(ItemType type, int level)
    {
        level++; // starts from 0
        if (type == ItemType.Food)
        {

            currentHealth += level * 1.5f * Constants.HEALTH_MULTIPLYER;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        } else if (type == ItemType.Shield)
        {
            currentArmor += level * 1.5f * Constants.ARMOR_MULTIPLYER;
            if (currentArmor > maxArmor)
            {
                currentArmor = maxArmor;
            }
        }

        UpdateUI();
    }
}
