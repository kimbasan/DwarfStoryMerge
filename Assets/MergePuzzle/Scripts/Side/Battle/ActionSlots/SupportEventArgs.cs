using System;

public class SupportEventArgs : EventArgs
{
    private ItemType itemType;
    private int level;

    public ItemType GetItemType() { return itemType; }
    public int GetLevel() { return level; }

    public SupportEventArgs(ItemType itemType, int level)
    {
        this.itemType = itemType;
        this.level = level;
    }
}
