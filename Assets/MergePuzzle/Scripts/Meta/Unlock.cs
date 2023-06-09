using System;
using UnityEngine;
using UnityEngine.UI;

public class Unlock : MonoBehaviour
{
    private static readonly string[] unlockNames = { Constants.ELF_UNLOCK_BOOL, Constants.HUMAN_UNLOCK_BOOL };

    [SerializeField] private GameObject unlockImage;
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private PlayerLeveling leveling;

    [SerializeField] private Text unlockHPBoostText;
    [SerializeField] private Text unlockCostText;

    public UnlockType unlock;
    public event EventHandler UnlockBought;

    private int unlockCost;
    private float unlockHPBoost;

    public void Awake()
    {
        switch (unlock)
        {
            case UnlockType.Elf:
                unlockCost = leveling.elfUnlockCost;
                unlockHPBoost = leveling.elfUnlockHealth;
                break;
            case UnlockType.Human: 
                unlockCost = leveling.humanUnlockCost;
                unlockHPBoost = leveling.humanUnlockHealth;
                break;
        }
        UpdateImage();
    }

    private void UpdateImage()
    {
        bool unlocked = PlayerPrefs.GetInt(unlockNames[(int)unlock]) > 0;
        if (unlocked)
        {
            unlockImage.SetActive(true);
            unlockButton.SetActive(false);

        } else
        {
            unlockHPBoostText.text = unlockHPBoost.ToString();
            unlockCostText.text = unlockCost.ToString();
        }
    }

    public void BuyUnlock()
    {
        int coins = PlayerPrefs.GetInt(Constants.SCORE_INT);

        if (coins >= unlockCost) {
            coins -= unlockCost;
            PlayerPrefs.SetInt(Constants.SCORE_INT, coins);
            PlayerPrefs.SetInt(unlockNames[(int)unlock], 1);
            UpdateImage();

            if (UnlockBought != null)
            {
                UnlockBought.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

public enum UnlockType
{
    Elf, Human
}
