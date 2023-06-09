using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private ItemType slotType;
    [SerializeField] private int levelRequired;

    public event EventHandler ItemUsed;

    public void OnDrop(PointerEventData eventData)
    {
        var mergableItem = eventData.pointerDrag.GetComponent<MergableItem>();
        if (mergableItem.GetItemType() == slotType && mergableItem.GetCurrentLevel() == levelRequired)
        {
            
            if (ItemUsed!= null)
            {
                var draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
                draggedItem.MergeAndDestroy();
                Debug.Log("Item Used");
                ItemUsed.Invoke(this, EventArgs.Empty);
            }
        } else
        {
            Debug.Log("Wrong item or level");
        }
    }

    public void SetRequiredItem(ItemType slotType, int level) {
        this.slotType = slotType;
        this.levelRequired = level;
    }
}
