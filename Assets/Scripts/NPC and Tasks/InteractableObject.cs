//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The types of items
    private enum ObjectType
    {
        Grave = 0,
        Grass = 1,
        Candle = 2,
        Flowerbed_Red = 3,
        Flowerbed_Orange = 4,
        Flowerbed_Blue = 5,
        Flowerbed_Turquoise = 6,
        Other = 99
    }

    [SerializeField] //This item's type
    private ObjectType objectType;

    [SerializeField] //The item needed to clear this object
    private ItemData neededItem;

    [SerializeField] //The NPC associated with this object
    private NPC questGiver;

    [SerializeField] //The main quest NPC associated with this object
    private MainQuest mainQuest;

    // Has this task been completed today?
    private bool isDone = false;

    //Get the ID of the task associated with this object's type
    public int GetID()
    {
        int id = 99;
        switch (objectType)
        {
            case ObjectType.Grave:
                id = questGiver.tasks.GetTaskID((int)ObjectType.Grave);
                break;
            case ObjectType.Grass:
                id = questGiver.tasks.GetTaskID((int)ObjectType.Grass);
                break;
            case ObjectType.Candle:
                id = questGiver.tasks.GetTaskID((int)ObjectType.Candle);
                break;
            case ObjectType.Flowerbed_Blue:
                id = questGiver.tasks.GetTaskID((int)ObjectType.Flowerbed_Blue);
                break;
            case ObjectType.Flowerbed_Orange:
                id = questGiver.tasks.GetTaskID((int)ObjectType.Flowerbed_Orange);
                break;
            case ObjectType.Flowerbed_Red:
                id = questGiver.tasks.GetTaskID((int)ObjectType.Flowerbed_Red);
                break;
            case ObjectType.Flowerbed_Turquoise:
                id = questGiver.tasks.GetTaskID((int)ObjectType.Flowerbed_Turquoise);
                break;
            default:
                break;
        }
        return id;
    }

    //The interaction
    public void Interaction(PlayerController player)
    {
        //if the task hasn't been cleared yet
        if (!isDone)
        {
            int index;

            //trigger the interactionHandler with the index of the task associated with this object's type
            switch (objectType)
            {
                case ObjectType.Grave:
                    index = (int)ObjectType.Grave;
                    if (player.inventory.ItemSelected(questGiver.tasks.GetNeededItem(index)))
                    {
                        if (questGiver.tasks.GetNeededItem(index).isConsumable)
                            player.inventory.RemoveItem(questGiver.tasks.GetNeededItem(index));
                        HandleTask(player, index);
                    }
                    else
                    {
                        //TODO: Do something when item not in inventory
                        Debug.Log(
                            "Need Item: " + questGiver.tasks.GetNeededItem(index).displayName
                        );
                    }
                    break;
                case ObjectType.Grass:
                    //Check for item
                    index = (int)ObjectType.Grass;
                    if (player.inventory.ItemSelected(questGiver.tasks.GetNeededItem(index)))
                    {
                        if (questGiver.tasks.GetNeededItem(index).isConsumable)
                            player.inventory.RemoveItem(questGiver.tasks.GetNeededItem(index));
                        HandleTask(player, index);
                    }
                    else
                    {
                        //TODO: Do something when item not in inventory
                        Debug.Log(
                            "Need Item: " + questGiver.tasks.GetNeededItem(index).displayName
                        );
                    }
                    break;
                case ObjectType.Candle:
                    //Check for item
                    index = (int)ObjectType.Candle;
                    if (player.inventory.ItemSelected(questGiver.tasks.GetNeededItem(index)))
                    {
                        if (questGiver.tasks.GetNeededItem(index).isConsumable)
                            player.inventory.RemoveItem(questGiver.tasks.GetNeededItem(index));
                        HandleTask(player, index);
                    }
                    else
                    {
                        //TODO: Do something when item not in inventory
                        Debug.Log(
                            "Need Item: " + questGiver.tasks.GetNeededItem(index).displayName
                        );
                    }
                    break;
                case ObjectType.Flowerbed_Blue:
                    //Check for item
                    index = (int)ObjectType.Flowerbed_Blue;
                    if (player.inventory.ItemSelected(questGiver.tasks.GetNeededItem(index)))
                    {
                        if (questGiver.tasks.GetNeededItem(index).isConsumable)
                            player.inventory.RemoveItem(questGiver.tasks.GetNeededItem(index));
                        HandleTask(player, index);
                    }
                    else
                    {
                        //TODO: Do something when item not in inventory
                        Debug.Log(
                            "Need Item: " + questGiver.tasks.GetNeededItem(index).displayName
                        );
                    }
                    break;
                case ObjectType.Flowerbed_Orange:
                    //Check for item
                    index = (int)ObjectType.Flowerbed_Orange;
                    if (player.inventory.ItemSelected(questGiver.tasks.GetNeededItem(index)))
                    {
                        if (questGiver.tasks.GetNeededItem(index).isConsumable)
                            player.inventory.RemoveItem(questGiver.tasks.GetNeededItem(index));
                        HandleTask(player, index);
                    }
                    else
                    {
                        //TODO: Do something when item not in inventory
                        Debug.Log(
                            "Need Item: " + questGiver.tasks.GetNeededItem(index).displayName
                        );
                    }
                    break;
                case ObjectType.Flowerbed_Red:
                    //Check for item
                    index = (int)ObjectType.Flowerbed_Red;
                    if (player.inventory.ItemSelected(questGiver.tasks.GetNeededItem(index)))
                    {
                        if (questGiver.tasks.GetNeededItem(index).isConsumable)
                            player.inventory.RemoveItem(questGiver.tasks.GetNeededItem(index));
                        HandleTask(player, index);
                    }
                    else
                    {
                        //TODO: Do something when item not in inventory
                        Debug.Log(
                            "Need Item: " + questGiver.tasks.GetNeededItem(index).displayName
                        );
                    }
                    break;
                case ObjectType.Flowerbed_Turquoise:
                    //Check for item
                    index = (int)ObjectType.Flowerbed_Turquoise;
                    if (player.inventory.ItemSelected(questGiver.tasks.GetNeededItem(index)))
                    {
                        if (questGiver.tasks.GetNeededItem(index).isConsumable)
                            player.inventory.RemoveItem(questGiver.tasks.GetNeededItem(index));
                        HandleTask(player, index);
                    }
                    else
                    {
                        //TODO: Do something when item not in inventory
                        Debug.Log(
                            "Need Item: " + questGiver.tasks.GetNeededItem(index).displayName
                        );
                    }
                    break;
                default:
                    player.anims.SetTrigger("Interact");
                    break;
            }
        }
    }

    //The handler for the interaction
    private void HandleTask(PlayerController player, int taskID)
    { //Trigger player animations
        player.anims.SetFloat("TaskID", taskID);
        player.anims.SetTrigger("DoTask");
        //If this is  associated with a normal NPC
        if (questGiver != null)
            questGiver.FinishTask();
        else if (mainQuest != null) //If this is instead associated with the main quest
            mainQuest.CandleLit();
        //Reset the player's interactable variable
        player.ResetInteractable();
        //Finish off this object
        FinishTask();
    }

    //This will switch the unfinished task model to the finished task model
    public void FinishTask()
    {
        isDone = true;
        //TODO: Replace this model with the cleared model


        //For now, just disappear
        this.gameObject.SetActive(false);
    }

    //Start Task (Triggered by NPC)
    public void StartTask()
    {
        //Reset done status
        isDone = false;

        //TODO: Replace this model with the uncleared model
        this.gameObject.SetActive(true);
    }
}
