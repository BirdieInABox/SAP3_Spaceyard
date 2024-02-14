using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<InvSlot> Container = new List<InvSlot>();

    [SerializeField]
    public Hotbar hotbar;

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
            Container.Add(new InvSlot(_item, _amount));
            Instantiate(_item.inventoryPrefab, Vector3.zero, Quaternion.identity, hotbar.transform);
        }
        else
        {
            hotbar.AddOrReduceItem(_item);
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

    public int FindItem(ItemData _item)
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
        bool isSelected = false;

        if (HasItem(_item))
        {
            if (Container[hotbar.index].item == _item)
            {
                isSelected = true;
            }
        }
        return isSelected;
    }

    public void RemoveItem(ItemData _item)
    {
        if (HasItem(_item))
        {
            Debug.Log("Here");
            int index = FindItem(_item);
            Container[index].ReduceAmount(1);
            if (Container[index].amount <= 0)
            {
                hotbar.RemoveItem(_item);
                Container.RemoveAt(index);
            }
            else
            {
                hotbar.AddOrReduceItem(_item);
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
