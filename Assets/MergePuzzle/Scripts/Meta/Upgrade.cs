using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public static readonly string[] unlockTypeNames = { Constants.HEALTH_LEVEL, Constants.START_ARMOR_LEVEL, Constants.MAX_ARMOR_LEVEL, Constants.CHEST_LEVEL };

    public UpgradeType upgradeType;

    [SerializeField] private PlayerLeveling leveling;

    [SerializeField] private Text currentValue;
    [SerializeField] private Text nextValue;
    [SerializeField] private Text upgradeCost;

    [SerializeField] private GameObject upgradeButton;

    private int cost;

    private List<float> upgrades;
    private List<int> costs;

    public event EventHandler BoughtUpgrade;

    private void Awake()
    {
        switch (upgradeType)
        {
            case UpgradeType.Health:
                upgrades = leveling.healthLevels;
                costs = leveling.healthCosts;
                break;
            case UpgradeType.StartArmor:
                upgrades = leveling.startingArmorLevels;
                costs = leveling.startingArmorCosts;
                break;
            case UpgradeType.MaxArmor: 
                upgrades = leveling.maxArmorLevels;
                costs= leveling.maxArmorCosts;
                break;
            case UpgradeType.Chest:
                upgrades= leveling.chestLevels;
                costs = leveling.chestCosts;
                break;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        currentValue.text = upgrades[PlayerPrefs.GetInt(unlockTypeNames[(int)upgradeType])].ToString();
        int nextLevel = PlayerPrefs.GetInt(unlockTypeNames[(int)upgradeType]) + 1;
        if (nextLevel == upgrades.Count)
        {
            nextValue.text = "Max";
            upgradeButton.SetActive(false);
        } else
        {
            nextValue.text = upgrades[PlayerPrefs.GetInt(unlockTypeNames[(int)upgradeType]) + 1].ToString();
            cost = costs[PlayerPrefs.GetInt(unlockTypeNames[(int)upgradeType]) + 1];
            upgradeCost.text = cost.ToString();
        }
    }

    public void BuyUpgrade()
    {
        int playerCoins = PlayerPrefs.GetInt(Constants.SCORE_INT);
        if (playerCoins >= cost)
        {
            playerCoins -= cost;
            PlayerPrefs.SetInt(Constants.SCORE_INT, playerCoins);
            var level = PlayerPrefs.GetInt(unlockTypeNames[(int)upgradeType]);
            PlayerPrefs.SetInt(unlockTypeNames[(int)upgradeType], level+1);

            UpdateText();
            if (BoughtUpgrade != null)
            {
                BoughtUpgrade.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

public enum UpgradeType
{
    Health, StartArmor, MaxArmor, Chest
}
