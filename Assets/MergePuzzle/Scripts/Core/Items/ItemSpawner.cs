using DG.Tweening;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private SlotRuntimeSet freeSlots;
    [SerializeField] private ItemFactory itemFactory;

    protected bool CreateRandomItemAndPlace()
    {
        bool placed = false;
        var slot = freeSlots.GetRandom();
        if (slot != null)
        {
            var item = itemFactory.CreateRandomItem();

            // transition animation


            var tween = DOTween.Sequence()
                .Append(item.transform.DOMove(transform.position, 0f))
                .Append(item.transform.DOMove(slot.transform.position, 0.3f));
            tween.Play();
            
            // attach to slot
            slot.SetItem(item);
           
            placed = true;
        }
        else
        {
            Debug.Log("No free slots");
        }
        return placed;
    }
}
