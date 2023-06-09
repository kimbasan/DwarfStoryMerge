using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] private Transform parentAfterDrag;

    [SerializeField] private Image image;
    [SerializeField] private MergableItem mergableItem;

    [SerializeField] private RectTransform rectTransform;
    private bool isMerging = false;

    private void OnEnable()
    {
        var tween = DOTween.Sequence()
            .SetEase(Ease.InQuint)
            .SetLink(gameObject)
            .Append(transform.DOScale(1.1f, 0.1f))
            .Append(transform.DOScale(1f, 0.2f));

        //tween.Play();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Mouse mouse = Mouse.current;
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        //transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isMerging)
        {
            transform.SetParent(parentAfterDrag);
            transform.SetAsFirstSibling();
            image.raycastTarget = true;
            rectTransform.anchoredPosition3D = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y, 0);

        }
    }

    public void UpdateParent(Transform newParent)
    {
        parentAfterDrag = newParent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.DOScale(1.1f, 0.2f).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.DOScale(1f, 0.2f).Play();
    }

    public void MergeAndDestroy()
    {
        isMerging = true;
        FreeUpParentSlot();

        var tween = DOTween.Sequence()
            .SetEase(Ease.InQuint)
            .SetLink(gameObject)
            .Append(transform.DOScale(0f, 0.3f))
            .OnComplete(DestroyObject);

        tween.Play();
    }

    public void FreeUpParentSlot()
    {
        var slot = parentAfterDrag.GetComponent<Slot>();
        if (slot != null)
        {
            slot.SetFree();
        }
        else
        {
            Debug.LogError("Cannot find slot component");
        }
    }

    public void Merge()
    {
        var tween = DOTween.Sequence()
            .SetLink(gameObject)
            .SetEase(Ease.InQuint)
            .Append(transform.DOScale(0f, 0.1f))
            .AppendCallback(Upgrade)
            .Append(transform.DOScale(1.2f, 0.1f))
            .Append(transform.DOScale(1f, 0.1f));

        tween.Play();
    }

    private void Upgrade()
    {
        mergableItem.Upgrade();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public ItemType GetItemType()
    {
        return mergableItem.GetItemType();
    }

    public int GetItemLevel()
    {
        return mergableItem.GetCurrentLevel();
    }

    public bool CanBeUpgraded()
    {
        return mergableItem.CanBeUpgraded();
    }
}
