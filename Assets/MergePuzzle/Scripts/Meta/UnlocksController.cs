using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlocksController : MonoBehaviour
{
    [SerializeField] private PlayerLeveling playerLeveling;



    [SerializeField] private Image elfUnlock;
    [SerializeField] private Image humanUnlock;

    private void Awake()
    {
    }

    private void UpdateText(Text text, List<float> levels, int currentLevel)
    {
        text.text = levels[currentLevel].ToString();
    }

    public void UpgradeHealth()
    {
        var currentHealthLevel = PlayerPrefs.GetInt(Constants.HEALTH_LEVEL);
        var healthList = playerLeveling.healthLevels;
        currentHealthLevel++;
        if (currentHealthLevel >= healthList.Count)
        {
        }
    }
}
