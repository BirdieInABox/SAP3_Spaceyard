using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hotbar : MonoBehaviour
{
    [SerializeField]
    private NewInventory inventory;
    public List<Transform> slots = new List<Transform>();

    private int numOfChildren;

    private int index = 0;
    private bool canScroll = true;

    [SerializeField]
    private float scrollCDTime = 0.15f;

    void Start()
    {
        numOfChildren = transform.childCount;
        foreach (Transform child in transform)
        {
            slots.Add(child);
        }
        HighlightSlot();
    }

    void Update()
    {
        CheckNumOfChildren();
    }

    private void CheckNumOfChildren()
    {
        if (transform.childCount > numOfChildren)
        {
            numOfChildren++;
            SortNewChild();
        }
        else if (transform.childCount < numOfChildren)
        {
            numOfChildren--;
        }
    }

    private void SortNewChild()
    {
        var lastChild = transform.GetChild(transform.childCount - 1);
        RectTransform childRT;
        foreach (var slot in slots)
        {
            if (slot.childCount == 1)
            {
                lastChild.SetParent(slot);
                lastChild.SetAsFirstSibling();
                childRT = lastChild.GetComponent<RectTransform>();
                childRT.offsetMin = new Vector2(0, 0);
                childRT.offsetMax = new Vector2(1, 1);
                childRT.localPosition = new Vector3(0, 0, 0);
                childRT.localScale = new Vector3(1, 1, 1);
                break;
            }
        }
    }

    private void ResetSlot()
    {
        slots[index].transform.localScale = new Vector3(1, 1, 1);
    }

    private void HighlightSlot()
    {
        slots[index].transform.localScale = new Vector3(1.15f, 1.15f, 1);
    }

    public void OnScrollWheel(InputValue value)
    {
        if (canScroll)
        {
            if ((index >= 0) && (index < slots.Count - 1) && (value.Get<Vector2>().y < 0))
            {
                ResetSlot();
                index++;
                HighlightSlot();
            }
            else if ((index <= slots.Count - 1) && (index > 0) && (value.Get<Vector2>().y > 0))
            {
                ResetSlot();
                index--;
                HighlightSlot();
            }
            StartCoroutine(ScrollCD());
        }
    }

    public IEnumerator ScrollCD()
    {
        canScroll = false;
        yield return new WaitForSeconds(scrollCDTime);
        canScroll = true;
    }
}
