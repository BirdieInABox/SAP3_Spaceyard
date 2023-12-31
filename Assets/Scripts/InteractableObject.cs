using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private enum ObjectType
    {
        Candle,
        Grave,
        Flowerbed,
        Grass
    }
    /* private enum InventoryItem
     {
         Lighter,
         Sponge,
         Flower,
         Sickle
     }*/

    [SerializeField] private ObjectType objectType;
    [SerializeField] private GameObject questGiver;
    // Start is called before the first frame update

    public void Interaction(GameObject player)
    {
        //Check for item
        /* switch (ObjectType)
         {
             Candle: 
             Grave:
             Flowerbed:
             Grass:
             default:
         }*/
        //Check if Questgiver wants this interaction
        //Player animation
        Debug.Log("I am an Interaction!");
    }
}
