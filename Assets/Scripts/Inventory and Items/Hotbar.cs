//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Hotbar : MonoBehaviour
{
    [SerializeField] //The inventory
    private InventorySystem inventory;

    //The list of inventory slots
    public List<Transform> slots = new List<Transform>();

    //The number of this object's children
    private int numOfChildren;

    //The index of the currently selected slot
    public int index = 0;

    //Enable/Disable scroll
    private bool canScroll = true;
    public bool isPaused = false;

    [SerializeField] //The amount of time needed between scrolls
    private float scrollCDTime = 0.15f;

    void Start()
    {
        //Count children
        numOfChildren = transform.childCount;
        //Add a slot for each child
        foreach (Transform child in transform)
        {
            slots.Add(child);
        }
        //Highlight the first slot
        HighlightSlot();
    }

    private void FixedUpdate()
    {
        //Keep a tap on the number of children
        CheckNumOfChildren();
    }

    //Check the number of children
    private void CheckNumOfChildren()
    {
        //If there is a new child
        if (transform.childCount > numOfChildren)
        {
            //Add numOfChildren to prevent multiple triggers of this
            numOfChildren++;
            //Sort the new child
            SortNewChild();
        }
        //If there ever happen to be less children
        else if (transform.childCount < numOfChildren)
        {
            //Reduce numOfChild to prevent multiple triggers of this
            numOfChildren--;
        }
    }

    //Sort the new child into its own hotbar slot
    private void SortNewChild()
    {
        //Get the last child in hierarchy of this object
        var lastChild = transform.GetChild(transform.childCount - 1);
        RectTransform childRT;
        //Check for an empty hotbar slot
        foreach (Transform slot in slots)
        {
            //If a slot is empty
            if (slot.childCount == 1)
            {
                //Put item into the slot
                lastChild.SetParent(slot);
                //Place behind Amount-number
                lastChild.SetAsFirstSibling();
                childRT = lastChild.GetComponent<RectTransform>();
                //Pack it neatly into the square and allow it to  grow when highlighted

                childRT.localPosition = Vector3.zero;
                childRT.localScale = Vector3.one;
                int _amount = inventory.Container[
                    inventory.FindItem(slot.GetComponentInChildren<InventoryItem>().GetData())
                ].amount;
                slot.GetComponentInChildren<TextMeshProUGUI>().SetText(_amount.ToString());
                //gtfo of here
                break;
            }
        }
    }

    public void RemoveItem(ItemData data)
    {
        Debug.Log("Remove");
        foreach (var slot in slots)
        {
            if (
                slot.childCount == 2
                && slot.GetComponentInChildren<InventoryItem>().GetData().itemID == data.itemID
            )
            {
                slot.GetComponentInChildren<TextMeshProUGUI>()
                    .SetText(
                        inventory.Container[
                            inventory.FindItem(
                                slot.GetComponentInChildren<InventoryItem>().GetData()
                            )
                        ].amount.ToString()
                    );

                Debug.Log(slot.GetComponentInChildren<InventoryItem>().transform.name);
                Destroy(slot.GetComponentInChildren<InventoryItem>().gameObject);
                break;
            }
        }
    }

    public void AddOrReduceItem(ItemData data)
    {
        Debug.Log("Reduce");
        foreach (var slot in slots)
        {
            if (
                slot.childCount == 2
                && slot.GetComponentInChildren<InventoryItem>().GetData() == data
            )
            {
                slot.GetComponentInChildren<TextMeshProUGUI>()
                    .SetText(
                        inventory.Container[
                            inventory.FindItem(
                                slot.GetComponentInChildren<InventoryItem>().GetData()
                            )
                        ].amount.ToString()
                    );
                break;
            }
        }
    }

    //When a slot is no longer highlighted, shrink it to normal size
    private void ResetSlot()
    {
        slots[index].transform.localScale = Vector3.one;
    }

    //When a slot is highlighted, make it grow slightly
    private void HighlightSlot()
    {
        slots[index].transform.localScale = new Vector3(1.15f, 1.15f, 1);
    }

    //Get inputs
    void OnGUI()
    {
        //If keyboard input
        if (!isPaused && Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            //Get the input as string
            string keyCode = Event.current.keyCode.ToString();

            //If the pressed key is a number, set index of hotbar to number pressed -1

            switch (keyCode)
            {
                case "Alpha1":
                    ResetSlot();
                    index = 0;
                    HighlightSlot();
                    break;
                case "Alpha2":
                    ResetSlot();
                    index = 1;
                    HighlightSlot();
                    break;
                case "Alpha3":
                    ResetSlot();
                    index = 2;
                    HighlightSlot();
                    break;
                case "Alpha4":
                    ResetSlot();
                    index = 3;
                    HighlightSlot();
                    break;
                case "Alpha5":
                    ResetSlot();
                    index = 4;
                    HighlightSlot();
                    break;
                case "Alpha6":
                    ResetSlot();
                    index = 5;
                    HighlightSlot();
                    break;
                case "Alpha7":
                    ResetSlot();
                    index = 6;
                    HighlightSlot();
                    break;
                case "Alpha8":
                    ResetSlot();
                    index = 7;
                    HighlightSlot();
                    break;
                case "Alpha9":
                    ResetSlot();
                    index = 8;
                    HighlightSlot();
                    break;
                default:
                    break;
            }
        }
    }

    //Unity Input System, getting the direction the scroll wheel is being scrolled in
    public void OnScrollWheel(InputValue value)
    {
        //If scrolling is not on cooldown
        if (!isPaused && canScroll)
        {
            //if scrolling towards the right and hasn't reached the end of the hotbar yet
            if ((index >= 0) && (index < slots.Count - 1) && (value.Get<Vector2>().y < 0))
            {
                //Reset the old slot before increasing the index
                ResetSlot();
                index++;
                //Highlight the new slot
                HighlightSlot();
            }
            //if scrolling towards the left and hasn't reached the end of the hotbar (index 0) yet
            else if ((index <= slots.Count - 1) && (index > 0) && (value.Get<Vector2>().y > 0))
            {
                //Reset the old slot before increasing the index
                ResetSlot();
                index--;
                //Highlight the new slot
                HighlightSlot();
            }
            //Put scrolling on a short cooldown
            StartCoroutine(ScrollCD());
        }
    }

    //Puts scrolling on a small cooldown (scrollCDTime) to allow for a smoother selection of the inventory slots
    public IEnumerator ScrollCD()
    {
        canScroll = false;
        yield return new WaitForSeconds(scrollCDTime);
        canScroll = true;
    }
}
