using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerLeveling")]
public class PlayerLeveling : ScriptableObject
{
    public List<float> healthLevels;
    public List<float> maxArmorLevels;
    public List<float> startingArmorLevels;
    [Header("Upgrade cost")]
    public List<int> healthCosts;
    public List<int> maxArmorCosts;
    public List<int> startingArmorCosts;

    [Header("Adds hp with unlock")]
    public float elfUnlockHealth;
    public float humanUnlockHealth;
    public int elfUnlockCost;
    public int humanUnlockCost;

    [Header("Increase chance to drop better loot")]
    public List<float> chestLevels;
    public List<int> chestCosts;
}
