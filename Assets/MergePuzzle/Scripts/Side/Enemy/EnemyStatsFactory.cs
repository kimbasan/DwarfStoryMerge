using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStatsFactory
{

    private List<ItemType> availableItems= new List<ItemType>();
    private float enemyDamage;
    public EnemyStatsFactory(PlayerLeveling leveling) {
        availableItems.Add(ItemType.Axe);
        if (PlayerPrefs.GetInt(Constants.ELF_UNLOCK_BOOL) > 0)
        {
            availableItems.Add(ItemType.Bow);
        }
        if (PlayerPrefs.GetInt(Constants.HUMAN_UNLOCK_BOOL) > 0)
        {
            availableItems.Add(ItemType.Sword);
        }

        enemyDamage = 10;
    }

    public EnemyStats CreateEnemyStats()
    {
        int enemyDifficulty = PlayerPrefs.GetInt(Constants.DIFFICULTY_MULTIPLYER);
        if (enemyDifficulty > 0)
        {
            enemyDamage *= enemyDifficulty;
        }
        // should use only items that can be used by player
        // on start - axes and shields only
        float itemChance = UnityEngine.Random.value;


        var items = new List<Item>();
        items.Add(new Item(availableItems[UnityEngine.Random.Range(0, availableItems.Count)], UnityEngine.Random.Range(1, 3)));
        for (int i = 0; i < enemyDifficulty; i++)
        {
            if (UnityEngine.Random.value > 0.5f)
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    items.Add(new Item(availableItems[UnityEngine.Random.Range(0, availableItems.Count)], UnityEngine.Random.Range(1, 3) + enemyDifficulty));
                } else
                {
                    items.Add(new Item(availableItems[UnityEngine.Random.Range(0, availableItems.Count)], UnityEngine.Random.Range(1, 3)));
                }
            }
        }

        return new EnemyStats(enemyDamage, items);
    }
}
[Serializable]
public class EnemyStats
{
    private float baseDamage;
    private List<Item> requiredItems;

    public float GetBaseDamage()
    {
        return baseDamage;
    }

    public List<Item> GetRequiredItems()
    {
        return requiredItems;
    }

    public EnemyStats(float damage, List<Item> itemsRequired)
    {
        baseDamage = damage;
        requiredItems = itemsRequired;
    }
}
