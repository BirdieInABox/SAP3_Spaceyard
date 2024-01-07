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
    private NPC questGiver;
    private bool isDone = false;

    // Start is called before the first frame update
    public int GetID()
    {
        int id = 99;
        switch (objectType)
        {
            case ObjectType.Grave:
                //Check for item
                id = tasks.GetTaskID((int)ObjectType.Grave);
                break;
            case ObjectType.Grass:
                //Check for item
                id = tasks.GetTaskID((int)ObjectType.Grass);
                break;
            case ObjectType.Candle:
                //Check for item
                id = tasks.GetTaskID((int)ObjectType.Candle);
                break;
            case ObjectType.Flowerbed:
                //Check for item
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
            Debug.Log("I am an Interaction!");
            switch (objectType)
            {
                case ObjectType.Grave:
                    //Check for item
                    HandleTask(player, tasks.GetTaskID((int)ObjectType.Grave));
                    break;
                case ObjectType.Grass:
                    //Check for item
                    HandleTask(player, tasks.GetTaskID((int)ObjectType.Grass));
                    break;
                case ObjectType.Candle:
                    //Check for item
                    HandleTask(player, tasks.GetTaskID((int)ObjectType.Candle));
                    break;
                case ObjectType.Flowerbed:
                    //Check for item
                    HandleTask(player, tasks.GetTaskID((int)ObjectType.Flowerbed));
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
