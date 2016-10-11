﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    // Return item
	public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    // Drag item unto new Slot
    #region IDropHandler implementation
    public void OnDrop (PointerEventData eventData)
    {
        if(!item)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged () );
        }
    }
    #endregion 
}
