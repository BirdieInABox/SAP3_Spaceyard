using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    public ItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(ItemData source)
    {
        data = source;
        IncreaseStack();
    }

    public void IncreaseStack()
    {
        stackSize++;
    }

    public void DecreaseStack()
    {
        stackSize--;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            transform.position.z
        );
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
    }
}
