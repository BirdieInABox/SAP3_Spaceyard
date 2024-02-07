//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
