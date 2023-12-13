using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private InventorySystem invSys;
    public ItemData referenceData;

    public void OnPickup()
    {
        invSys.AddItem(referenceData);
        Destroy(gameObject);
    }

}
