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
            StartCoroutine(GiveItems(player));
            interactable = false;
        }
    }

    private IEnumerator GiveItems(Player player)
    {
        foreach (ItemData item in itemsGiven)
        {
            player.AddItem(item);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
