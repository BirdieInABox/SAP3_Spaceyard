using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private Tasks tasks;

    //FIXME: connect this choice to the list of tasks in Tasks.cs
    private enum ObjectType
    {
        Grave = 0,
        Grass = 1,
        Candle = 2,
        Flowerbed = 3,
        Other = 99
    }

    [SerializeField]
    private ObjectType objectType;

    [SerializeField]
    private ItemData neededItem;

    [SerializeField]
    private NPC questGiver;
    private bool isDone = false;

    // Start is called before the first frame update
    public int GetID()
    {
        int id = 99;
        switch (objectType)
        {
            case ObjectType.Grave:
                id = tasks.GetTaskID((int)ObjectType.Grave);
                break;
            case ObjectType.Grass:
                id = tasks.GetTaskID((int)ObjectType.Grass);
                break;
            case ObjectType.Candle:
                id = tasks.GetTaskID((int)ObjectType.Candle);
                break;
            case ObjectType.Flowerbed:
                id = tasks.GetTaskID((int)ObjectType.Flowerbed);
                break;
            default:
                break;
        }
        return id;
    }

    public void Interaction(Player player)
    {
        if (!isDone)
        {
            int index;
            Debug.Log("I am an Interaction!");
            switch (objectType)
            {
                case ObjectType.Grave:
                    index = (int)ObjectType.Grave;
                    if (player.inventory.HasItem(tasks.GetNeededItem(index)))
                        HandleTask(player, tasks.GetTaskID(index));
                    else
                    {
                        //FIXME: Do something when item not in inventory
                        Debug.Log("Need Item: " + tasks.GetNeededItem(index).displayName);
                    }
                    break;
                case ObjectType.Grass:
                    //Check for item
                    index = (int)ObjectType.Grass;
                    if (player.inventory.HasItem(tasks.GetNeededItem(index)))
                        HandleTask(player, tasks.GetTaskID(index));
                    else
                    {
                        //FIXME: Do something when item not in inventory
                        Debug.Log("Need Item: " + tasks.GetNeededItem(index).displayName);
                    }
                    break;
                case ObjectType.Candle:
                    //Check for item
                    index = (int)ObjectType.Candle;
                    if (player.inventory.HasItem(tasks.GetNeededItem(index)))
                        HandleTask(player, tasks.GetTaskID(index));
                    else
                    {
                        //FIXME: Do something when item not in inventory
                        Debug.Log("Need Item: " + tasks.GetNeededItem(index).displayName);
                    }
                    break;
                case ObjectType.Flowerbed:
                    //Check for item
                    index = (int)ObjectType.Flowerbed;
                    if (player.inventory.HasItem(tasks.GetNeededItem(index)))
                        HandleTask(player, index);
                    else
                    {
                        //FIXME: Do something when item not in inventory
                        Debug.Log("Need Item: " + tasks.GetNeededItem(index).displayName);
                    }
                    break;
                default:
                    player.anims.SetTrigger("Interact");
                    break;
            }
        }
    }

    private void HandleTask(Player player, int taskID)
    {
        player.anims.SetFloat("TaskID", taskID);
        player.anims.SetTrigger("DoTask");
        questGiver.FinishTask();
        player.ResetInteractable();
        FinishTask();
    }

    public void FinishTask()
    {
        isDone = true;
        //FIXME: Replace this model with the cleared model



        this.gameObject.SetActive(false);
    }

    public void StartTask()
    {
        isDone = false;
        //FIXME: ADD TO JOURNAL

        //FIXME: Replace this model with the uncleared model
        this.gameObject.SetActive(true);
    }
}
