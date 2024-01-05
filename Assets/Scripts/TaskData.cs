using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "TaskData", order = 0)]
public class TaskData : ScriptableObject
{
    public string taskName;
    public int taskID;
    public GameObject prefab;
}
