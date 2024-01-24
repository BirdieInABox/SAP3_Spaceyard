//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* TODO: Cleanup
public struct TaskData
{
    public int neededItemID;
    public string taskName;
}

public class Tasks : Dictionary<int, TaskData>
{
    public void Add(int key, int val1, string val2)
    {
        TaskData data;
        data.neededItemID = val1;
        data.taskName = val2;
        this.Add(key, data);
    }
}
*/
public class Tasks : MonoBehaviour
{
    [SerializeField] //Array of tasks
    public TaskData[] tasks;

    private void Awake()
    {
        //Sort the array
        var taskdata = new List<TaskData>(tasks);
        taskdata.Sort((a, b) => a.taskID.CompareTo(b.taskID));
        tasks = taskdata.ToArray();
    }

    //Gets size of tasks array
    public int NumOfTasks()
    {
        return tasks.Length;
    }

    //Get task name at Index
    public string GetTaskName(int index)
    {
        return tasks[index].taskName;
    }

    //Get task ID at Index
    public int GetTaskID(int index)
    {
        return tasks[index].taskID;
    }

    //Get needed item at Index
    public ItemData GetNeededItem(int index)
    {
        return tasks[index].neededItem;
    }

    public int GetTaskIndex(int id)
    {
        int index = 0;
        foreach (TaskData task in tasks)
        {
            if (task.taskID == id)
            {
                break;
            }
            else
                index++;
        }
        return index;
    }

    //Get tasks array
    public TaskData[] GetTaskList()
    {
        return tasks;
    }
}
/* TODO: Cleanup
    [SerializeField]
    private InventoryItem[] neededItem;

    [SerializeField]
    private int startIndex = 2;
    Tasks<int, TaskData> task = new Tasks<int, TaskData>();

    void Start()
    {
        int key = startIndex;
        int i = 0;

        foreach (string name in taskNames)
        {
            task.Add(key, neededItem[i].GetData().itemID, name);
            i++;
            key++;
        }
    }
}*/
