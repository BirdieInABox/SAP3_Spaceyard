//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//SCRIPTABLE OBJECT VERSION OF THE INVENTORY, TODO:POSSIBLY OBSOLETE?
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    public List<InvSlot> Container = new List<InvSlot>();

    [SerializeField]
    private int hotBarSlots;

    public void AddItem(ItemData _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            if (Container.Count < hotBarSlots)
            {
                Container.Add(new InvSlot(_item, _amount));
                Instantiate(_item.inventoryPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            { //FIXME: What happens when max slots arrived? Add inventory?}
            }
        }
    }

    public bool HasItem(ItemData _item)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            hasItem = Container[i].item == _item;
            if (hasItem)
                break;
        }
        return hasItem;
    }
}
/*
[System.Serializable]
public class InvSlot
{
    public ItemData item;
    public int amount;

    public InvSlot(ItemData _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}*/
