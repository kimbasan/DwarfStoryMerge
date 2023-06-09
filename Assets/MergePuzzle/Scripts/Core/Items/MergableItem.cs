using UnityEngine;
using UnityEngine.UI;

public class MergableItem : MonoBehaviour
{
    
    [SerializeField] private Image image;

    [Header("Debug")]
    [SerializeField] private int currentLvl = 0;
    [SerializeField] private ItemData itemData;

    [SerializeField] private AudioSource mergeSound;

    private void Awake()
    {
        if (itemData != null)
        {
            Init();
        }
    }

    public void Init()
    {
        this.image.sprite = itemData.levelSprites[currentLvl];
    }

    public void Init(ItemData itemData,int level = 0)
    {
        this.itemData = itemData;
        this.currentLvl = level;
        this.image.sprite = itemData.levelSprites[level];
    }

    public bool CanBeUpgraded()
    {
        return currentLvl < itemData.GetMaxLevel();
    }

    public int GetCurrentLevel()
    {
        return currentLvl;
    }

    public ItemType GetItemType()
    {
        return itemData.itemType;
    }

    public void Upgrade()
    {
        currentLvl++;
        this.image.sprite = itemData.levelSprites[currentLvl];
        mergeSound.Play();
    }
}
