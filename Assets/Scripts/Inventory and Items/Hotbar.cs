//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        foreach (var slot in slots)
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
                //gtfo of here
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

    //Unity Input System, getting the direction the scroll wheel is being scrolled in
    public void OnScrollWheel(InputValue value)
    {
        //If scrolling is not on cooldown
        if (canScroll)
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
