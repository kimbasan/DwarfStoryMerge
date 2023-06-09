using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CharacterAnimationSet enemyMovement;

    [SerializeField] private ItemCatalog itemCatalog;

    [SerializeField] private CharacterAnimation animatator;

    [SerializeField] private List<Image> imagesOfItemsRequired;
    private List<Item> itemsRequired = new List<Item>();

    [SerializeField] private EnemyDropSpawner enemyDrop;

    [Header("Debug")]
    [SerializeField] private EnemyStats currentStats;

    private int currentItemIndex;

    private void OnEnable()
    {
        enemyMovement.Add(animatator);
    }

    private void OnDisable()
    {
        enemyMovement.Remove(animatator);
    }

    public void Init(EnemyStats stats)
    {
        itemsRequired.Clear();
        currentStats = stats;

        int imageIndex = 0;
        // enable images that show items
        foreach (var item in stats.GetRequiredItems()) {

            ItemData data = FindItemData(item.GetItemType());
            
            if (data == null) {
                Debug.LogError("item data not found!");
            } else
            {
                itemsRequired.Add(item);

                var image = imagesOfItemsRequired[imageIndex];
                image.sprite = data.levelSprites[item.GetLevel()];
                image.gameObject.SetActive(true);
                imageIndex++;
            }
        }

        gameObject.layer = Constants.DEFAULT_LAYER;

        currentItemIndex = 0;
    }

    private ItemData FindItemData(ItemType type)
    {
        ItemData requiredItemData = null;
        foreach (var itemData in itemCatalog.itemDataList)
        {
            if (itemData.itemType == type)
            {
                requiredItemData = itemData;
                break;
            }
        }
         return requiredItemData;
    }


    public float Attack()
    {
        // play attack animation
        animatator.Attack();
        return GetDamage();
    }

    public bool TakeHit()
    {
        imagesOfItemsRequired[currentItemIndex].gameObject.SetActive(false);

        currentItemIndex++;

        bool isDead = false;
        if (currentItemIndex > 3 || !imagesOfItemsRequired[currentItemIndex].isActiveAndEnabled)
        {
            isDead = true;
        } else
        {
            animatator.TakeDamage();
        }
        return isDead;
    }

    public float GetDamage()
    {
        return currentStats.GetBaseDamage();
    }

    public void Stop()
    {
        animatator.StopRunning();
    }

    public void Die()
    {
        animatator.Die();
        gameObject.layer = Constants.DEAD_ENEMY_LAYER;
        enemyDrop.CreateDropItem();
    }

    public Item GetRequiredItem()
    {
        return itemsRequired[currentItemIndex];
    }
}