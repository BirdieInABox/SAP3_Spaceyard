//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour /*,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IInitializePotentialDragHandler,
        IDropHandler*/
{
    [SerializeField] //The ItemData associated with this Item
    private ItemData data;
    public Transform parentAfterDrag { get; set; } //Temp Parent Object
    private CanvasGroup canvasGroup; //Component responsible for letting through raycasts
    private RectTransform rectTransform; //The item's transform data

    //The current parent
    private Transform parent;

    //The canvas the object is first added to
    [SerializeField]
    private Canvas uiCanvas;

    private void Awake()
    {
        gameObject.GetComponent<Image>().sprite = data.icon;
        //Get components
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //Return this item's data
    public ItemData GetData()
    {
        return data;
    }

    /* CANCELLED: DRAG & DROP SYSTEM, AS WE HAVE MORE ITEM SLOTS THAN ITEMS IN THE GAME
        public void OnBeginDrag(PointerEventData eventData)
        {
            //Temporarily safe the old parent
            parentAfterDrag = transform.parent;
            //set parent to the inventory canvas
            transform.SetParent(transform.root.root);
            //Show this over every other element of the inventory
            transform.SetAsLastSibling();
            //make this invisible for raycasts
            canvasGroup.blocksRaycasts = false;
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            //FIXME: Keep this at the cursor's position
            //FIXME: figure out scaling for dragging the item
            transform.position = eventData.position;
            //transform.Translate(eventData.position * uiCanvas.transform.localScale);
            // (eventData.delta * uiCanvas.transform.localScale);
            //Debug.Log(eventData.delta * uiCanvas.transform.localScale);
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            //reset parent to what it was before, if no new parent has been chosen
            transform.SetParent(parentAfterDrag);
            //Reactivate raycast
            canvasGroup.blocksRaycasts = true;
            //Reset localPosition
            rectTransform.localPosition = new Vector3(0, 0, 0);
        }
    
        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            eventData.useDragThreshold = false;
        }
    
        public void OnDrop(PointerEventData eventData) { }*/
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
