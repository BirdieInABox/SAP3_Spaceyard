//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //The Item associated with this
    public ItemData data;

    [SerializeField]
    private PlayerController player;

    //Determine whether or not the item spawns
    public void StartTime(bool isDay)
    {
        if (isDay == data.daySpawning)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    //Called on Pickup
    public void OnPickup()
    {
        //Possible Animation here
        Pickup();
    }

    protected virtual void Pickup()
    {
        player.ResetPickup();
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }
}
