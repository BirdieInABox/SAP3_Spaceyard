//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKey : PickupItem
{
    [SerializeField] //The gate that gets opened
    private GameObject gate;

    //Delete this and open gate
    protected override void Pickup()
    {
        Destroy(this);
        gate.GetComponent<BoxCollider>().enabled = false;
    }
}
