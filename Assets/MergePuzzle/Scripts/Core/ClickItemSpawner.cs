using UnityEngine;
using UnityEngine.EventSystems;

public class ClickItemSpawner : ItemSpawner, IPointerClickHandler
{
    [SerializeField] private int numberOfUses;
    [SerializeField] private bool unlimited;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (unlimited)
        {
            CreateRandomItemAndPlace();
        } else if (numberOfUses > 0)
        {
            CreateRandomItemAndPlace();
            numberOfUses--;
        } else
        {
            Debug.Log("Chest expired");
            var drag = GetComponent<DraggableItem>();
            if (drag != null)
            {
                drag.FreeUpParentSlot();
                gameObject.SetActive(false);
                Destroy(gameObject);
            } else
            {
                Debug.LogError("Can't free up parent slot");
            }
            
        }
        
    }
}
