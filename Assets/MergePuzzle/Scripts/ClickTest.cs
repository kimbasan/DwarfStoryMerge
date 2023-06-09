using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //private Vector2 mousePosition;

    private float offsetX, offsetY;
    private static bool mouseReleased;


    private void OnMouseDown()
    {
        Debug.Log("mouse down");
        mouseReleased = false;

        Mouse mouse = Mouse.current;
        Vector2 mousePosition = mouse.position.ReadValue();
        Vector2 cameraMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        offsetX = cameraMousePosition.x - transform.position.x;
        offsetY = cameraMousePosition.y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        //Debug.Log("OnMouseDrag");
        Mouse mouse = Mouse.current;
        Vector2 mousePosition = mouse.position.ReadValue();
        Vector2 cameraMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector2(cameraMousePosition.x - offsetX, cameraMousePosition.y - offsetY);
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse up");
        mouseReleased = true;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("pointer enter");
        //onEnter.Play();
        transform.DOScale(1.1f, 0.2f).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("pointer exit");
        //onExit.Play();
        transform.DOScale(1f, 0.2f).Play();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }




    /*private void Update()
    {
        //cam = Camera.main;
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Debug.Log("in raycast");
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.ToString());
            }
        }
    }
    */
}
