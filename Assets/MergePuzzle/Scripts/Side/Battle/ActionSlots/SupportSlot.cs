using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SupportSlot : MonoBehaviour, IDropHandler
{
    public event EventHandler SupportUsed;

    //[SerializeField] private Image lockedTimer;
    private bool locked = false;

    private void Awake()
    {
        //lockedTimer.gameObject.SetActive(false);
        //lockedTimer.fillAmount= 1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!locked)
        {
            var mergable = eventData.pointerDrag.GetComponent<MergableItem>();
            if (mergable.GetItemType() == ItemType.Food || mergable.GetItemType() == ItemType.Shield)
            {
                if (SupportUsed != null)
                {
                    SupportUsed.Invoke(this, new SupportEventArgs(mergable.GetItemType(), mergable.GetCurrentLevel()));

                    var draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
                    draggedItem.MergeAndDestroy();
                    Debug.Log("Support item Used");

                    //locked= true;
                    //lockedTimer.gameObject.SetActive(true);
                }
            }
        }
    }
    
    public void UpdateTimer(float fillAmount)
    {
        //lockedTimer.fillAmount = fillAmount;
    }

    public void Unlock()
    {
        locked = false;
        //lockedTimer.fillAmount = 1;
        //lockedTimer.gameObject.SetActive(false);

    }
}
