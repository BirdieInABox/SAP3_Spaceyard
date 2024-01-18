using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private ItemData[] itemsGiven;
    private bool interactable = true;

    public void Interaction(Player player)
    {
        if (interactable)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Open");
            foreach (ItemData item in itemsGiven)
            {
                player.AddItem(item);
                interactable = false;
            }
        }
    }
}
