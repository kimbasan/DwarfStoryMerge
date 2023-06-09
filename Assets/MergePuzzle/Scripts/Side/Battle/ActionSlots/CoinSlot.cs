using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinSlot : MonoBehaviour, IDropHandler
{
    public event EventHandler CoinAdded;

    public void OnDrop(PointerEventData eventData)
    {
        var mergable = eventData.pointerDrag.GetComponent<MergableItem>();
        if (mergable != null)
        {
            if (mergable.GetItemType() == ItemType.Coin)
            {
                if (CoinAdded != null)
                {
                    CoinAdded.Invoke(this, new SupportEventArgs(mergable.GetItemType(), mergable.GetCurrentLevel()));
                    
                    var draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
                    draggedItem.MergeAndDestroy();
                    Debug.Log("Coin item Used");
                }
            }
        }

    }
}
