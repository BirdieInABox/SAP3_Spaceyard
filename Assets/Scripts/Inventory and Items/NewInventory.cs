using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInventory : MonoBehaviour
{
    public List<InvSlot> Container = new List<InvSlot>();

    [SerializeField]
    private Hotbar hotbar;

    private int hotbarSlots;

    private void Start()
    {
        hotbarSlots = hotbar.transform.childCount;
    }

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
            if (Container.Count < hotbarSlots)
            {
                Container.Add(new InvSlot(_item, _amount));
                Instantiate(
                    _item.inventoryPrefab,
                    Vector3.zero,
                    Quaternion.identity,
                    hotbar.transform
                );
            }
            else
            { //FIXME: What happens when max slots arrived? Add inventory?}
            }
        }
    }

    public bool HasItem(ItemData _item)
    {
        bool _hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            _hasItem = Container[i].item == _item;
            if (_hasItem)
                break;
        }
        return _hasItem;
    }

    private int FindItem(ItemData _item)
    {
        int _index = 0;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                _index = i;
                break;
            }
        }
        return _index;
    }

    public bool ItemSelected(ItemData _item)
    {
        bool _hasItem = false;
        for (int i = 0; i < hotbar.slots.Count; i++)
        {
            if (hotbar.slots[i].childCount > 1)
                _hasItem =
                    hotbar.slots[i].GetChild(0).GetComponent<InventoryItem>().GetData() == _item;
            if (_hasItem)
                break;
        }
        return _hasItem;
    }

    public void RemoveItem(ItemData _item)
    {
        if (HasItem(_item))
        {
            int index = FindItem(_item);
            Container[index].ReduceAmount(1);
            if (Container[index].amount <= 0)
            {
                Container.RemoveAt(index);
            }
        }
    }
}

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

    public void ReduceAmount(int value)
    {
        amount -= value;
    }
}
