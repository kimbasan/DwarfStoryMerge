using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private PlayerLeveling leveling;
    public List<ItemSpawnSettings> itemSpawnSettings;
    public GameObject itemPrefab;
    public GameObject spawnerPrefab;

    private float finalWeight = 0f;

    private bool bowUnlocked = false;
    private bool swordUnlocked = false;

    private float chestChance = 0f;

    private void Awake()
    {
        // init weights
        bowUnlocked = PlayerPrefs.GetInt(Constants.ELF_UNLOCK_BOOL) > 0;
        swordUnlocked = PlayerPrefs.GetInt(Constants.HUMAN_UNLOCK_BOOL) > 0;

        chestChance = leveling.chestLevels[PlayerPrefs.GetInt(Constants.CHEST_LEVEL)];

        if (!bowUnlocked || !swordUnlocked)
        {
            for (int i = itemSpawnSettings.Count -1; i >= 0; i-- )
            {
                if ((!bowUnlocked && itemSpawnSettings[i].GetData().itemType == ItemType.Bow) || (!swordUnlocked && itemSpawnSettings[i].GetData().itemType == ItemType.Sword))
                {
                    itemSpawnSettings.RemoveAt(i);
                }
            }
        }

        InitWeights();
    }

    public GameObject CreateRandomItem()
    {
        ItemData itemData = GetRandomItemData();

        GameObject newItem = null;
        if (itemData.itemType == ItemType.CoinChest)
        {
            newItem = Instantiate(spawnerPrefab);
        } else
        {
            newItem = Instantiate(itemPrefab);
        }

        var mergable = newItem.GetComponent<MergableItem>();
        if (mergable != null)
        {
            mergable.Init(itemData, GetRandomLevel());
        }
        
        return newItem;
    }

    private ItemData GetRandomItemData()
    {
        var randValue = Random.value * finalWeight;
        ItemData data = null;
        foreach(var item in itemSpawnSettings )
        {
            if (item.GetWeight() >= randValue && item.GetPrevWeight() <= randValue)
            {
                data = item.GetData();
            }
        }
        if (data == null)
        {
            Debug.Log("Could not roll for random item data");
            data = itemSpawnSettings[0].GetData();
        }
        return data;
    }

    private int GetRandomLevel()
    {
        int level = 0;
        if (chestChance > 0)
        {
            if (chestChance > Random.value)
            {
                level = 1;
            }
        }
        return level;
    }

    private void InitWeights()
    {
        float prevWeight = 0;
        foreach(var item in itemSpawnSettings) {
            finalWeight += item.GetSpawnChance();
            item.SetWeight(finalWeight);
            item.SetPrevWeight(prevWeight);
            prevWeight = finalWeight;
        }
    }
}

[Serializable]
public class ItemSpawnSettings
{
    [SerializeField] private ItemData data;
    [SerializeField] [Range(1f,100f)] private float spawnChance;
    private float lowerWeight;
    private float weight;

    public ItemData GetData() { return data; }
    public float GetSpawnChance() { return spawnChance; }

    public float GetWeight() { return weight; }

    public float GetPrevWeight() { return lowerWeight; }

    public void SetWeight(float weight)
    {
        this.weight = weight;
    } 

    public void SetPrevWeight(float prevWeight)
    {
        this.lowerWeight = prevWeight;
    }
}