//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string displayName;
    public int itemID;
    public Sprite icon;
    public bool isConsumable;

    public bool daySpawning;

    [TextArea(15, 20)]
    public string description;
    public GameObject pickupPrefab,
        inventoryPrefab;
}
