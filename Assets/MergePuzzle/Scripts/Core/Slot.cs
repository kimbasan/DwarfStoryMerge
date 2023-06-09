using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public SlotRuntimeSet freeSlots;
    public SlotRuntimeSet occupiedSlots;

    [SerializeField] private GameObject stoneImage;

    [Header("Debug")]
    [SerializeField]
    private SlotState state;
    [SerializeField]
    private DraggableItem currentItem;

    public SlotState GetState() { return state; }

    public event EventHandler MergeHappened;

    public void OnDrop(PointerEventData eventData)
    {
        if (state == SlotState.Blocked)
        {
            Debug.Log("Attempt to move to blocked tile");
        } else
        {
            GameObject obj = eventData.pointerDrag;
            DraggableItem draggedItem = obj.GetComponent<DraggableItem>();

            if (currentItem != null && draggedItem.Equals(currentItem))
            {
                return;
            }

            if (state == SlotState.Free)
            {
                if (draggedItem != null)
                {
                    draggedItem.FreeUpParentSlot();
                    draggedItem.UpdateParent(transform);

                    currentItem = draggedItem;
                    SetOccupied();
                }
            }
            else
            {
                if (currentItem != null && draggedItem != null)
                {
                    // check if same type
                    if (currentItem.GetItemType() == draggedItem.GetItemType())
                    {
                        if (currentItem.GetItemLevel() == draggedItem.GetItemLevel())
                        {

                            if (currentItem.CanBeUpgraded())
                            {
                                currentItem.Merge();
                                draggedItem.MergeAndDestroy();
                                Debug.Log("Merge");

                                if (MergeHappened != null)
                                {
                                    MergeHappened.Invoke(this, new MergeEventArgs(currentItem.GetItemType()));
                                }

                            }
                            else
                            {
                                Debug.Log("Max level reached");
                            }


                        }
                        else
                        {
                            Debug.Log("Different Level");
                        }
                    }
                    else
                    {
                        Debug.Log("Different types");
                    }
                }
                else
                {
                    Debug.LogError("Draggable not set");
                }

            }
        }
    }

    public void SetFree()
    {
        state = SlotState.Free;
        stoneImage.SetActive(false);
        
        occupiedSlots.Remove(this);
        freeSlots.Add(this);
        currentItem = null;
    }

    public void SetOccupied()
    {
        state = SlotState.Occupied;

        freeSlots.Remove(this);
        occupiedSlots.Add(this);
    }


    public void SetItem(GameObject item)
    {
        SetOccupied();

        item.transform.SetParent(transform, false);
        item.transform.SetAsFirstSibling();

        currentItem = item.GetComponent<DraggableItem>();
        currentItem.UpdateParent(transform);
    }

}

public enum SlotState
{
    NotSet, Blocked, Free, Occupied
}