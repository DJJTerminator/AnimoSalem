using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    #region IBeginDragHandler implementation

    //Return Drag Object back to proper spot
    public void OnBeginDrag (PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

    #endregion

    #region IDragHandler implementation

    // Move object with mouse
    public void OnDrag (PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    #endregion

    #region IEndDragHandler implementation

    // Object stay put after being drag to specific spot
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
    }

    #endregion 
}
