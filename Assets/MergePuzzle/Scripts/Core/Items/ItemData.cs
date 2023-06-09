using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public List<Sprite> levelSprites;

    public int GetMaxLevel()
    {
        if (levelSprites != null)
        {
            return levelSprites.Count - 1;
        } else
        {
            return 0;
        }
    }
}
