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
    public Transform parentAfterDrag { get; set; }
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    [SerializeField]
    private Canvas uiCanvas;

    public ItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(ItemData source)
    {
        data = source;
        IncreaseStack();
    }

    public ItemData GetData()
    {
        return data;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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
