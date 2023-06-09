using System;

public class MergeEventArgs : EventArgs
{
    private ItemType mergeType;
    public ItemType GetMergeType() { return mergeType; }

    public MergeEventArgs(ItemType mergeType)
    {
        this.mergeType = mergeType;
    }
}
