//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //The Item associated with this
    public ItemData data;

    //Called on Pickup
    public void OnPickup()
    {
        //Possible Animation here

        Destroy(gameObject);
    }
}
