using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData data;
    

    public void OnPickup()
    {
        Destroy(gameObject);
    }
}
