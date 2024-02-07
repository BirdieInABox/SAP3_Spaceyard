//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] //Items in chest
    private ItemData[] itemsGiven;
    private bool interactable = true;

    //Called when player interacts
    public void Interaction(PlayerController player)
    {
        //If not opened yet
        if (interactable)
        {
            //play open-animation
            this.gameObject.GetComponent<Animator>().SetTrigger("Open");
            //Gives items
            StartCoroutine(GiveItems(player));
            interactable = false;
        }
    }

    private IEnumerator GiveItems(PlayerController player)
    {
        //go through every item in the chest
        foreach (ItemData item in itemsGiven)
        {
            //Add the item to the player's inventory
            player.AddItem(item);
            //Delay to prevent overwriting of items
            yield return new WaitForSeconds(0.1f);
        }
    }
}
