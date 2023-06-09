using System;

[Serializable]
public struct Item
{
    private ItemType type;
    private int level;
    public int GetLevel()
    {
        return level;
    }

    public ItemType GetItemType()
    {
        return type;
    }
    public Item(ItemType type, int level)
    {
        this.type = type;
        this.level = level;
    }
}
