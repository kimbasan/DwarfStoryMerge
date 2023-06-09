using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private Text playerCoins;
    [SerializeField] private List<Upgrade> upgrades;
    [SerializeField] private List<Unlock> unlocks;

    private void Awake()
    {
        UpdateCoins();
        foreach(var upgrade in upgrades)
        {
            upgrade.BoughtUpgrade += Upgrade_BoughtUpgrade;
        }
        foreach(var unlock in unlocks)
        {
            unlock.UnlockBought += Unlock_UnlockBought;
        }
    }

    private void Unlock_UnlockBought(object sender, System.EventArgs e)
    {
        UpdateCoins();
    }

    private void Upgrade_BoughtUpgrade(object sender, System.EventArgs e)
    {
        UpdateCoins();
    }

    private void UpdateCoins()
    {
        playerCoins.text = PlayerPrefs.GetInt(Constants.SCORE_INT).ToString();
    }
}
