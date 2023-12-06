using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
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
}
