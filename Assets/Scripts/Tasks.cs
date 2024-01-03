using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
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
    [SerializeField]
    public TaskData[] tasks;

    public int NumOfTasks()
    {
        return tasks.Length;
    }

    public string GetTaskName(int index)
    {
        return tasks[index].taskName;
    }

    public int GetTaskID(int index)
    {
        return tasks[index].taskID;
    }

    public TaskData[] GetTaskList()
    {
        return tasks;
    }
}
/*
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
