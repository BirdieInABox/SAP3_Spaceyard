using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem
    : MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IInitializePotentialDragHandler,
        IDropHandler
{
    /*public enum GlobalEvent
    {
        
    }*/
    public Transform parentAfterDrag { get; set; }
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    [SerializeField]
    private Transform parent;

    [SerializeField]
    private Canvas uiCanvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //FIXME:  Messenger.Brodacast<Transform>("ItemPickedUp", rectTransform);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //FIXME: figure out scaling for dragging the item
        rectTransform.anchoredPosition += eventData.delta;
        // transform.Translate(eventData.delta * uiCanvas.transform.localScale);
        // (eventData.delta * uiCanvas.transform.localScale);
        //Debug.Log(eventData.delta * uiCanvas.transform.localScale);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;

        rectTransform.anchoredPosition = new Vector3(0, 0, 0);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void OnDrop(PointerEventData eventData) { }
}


/*
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/Items", order = 0)]
public class InventoryItem : ScriptableObject
{
    public ItemData data;

    [TextArea(15, 20)]
    public string description;
}
*/
