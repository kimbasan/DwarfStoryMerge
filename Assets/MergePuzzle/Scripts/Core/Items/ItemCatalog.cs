using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/ItemCatalog")]
public class ItemCatalog : ScriptableObject
{
    public List<ItemData> itemDataList;
}
