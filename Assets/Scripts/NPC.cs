using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private static int dialogueID,
        lineID;
    private string[] lines;

    [System.Serializable]
    public class Dialogues
    {
        public string[] lines = new string[lineID];
    }

    public Dialogues[] dialogues = new Dialogues[dialogueID];

    [SerializeField]
    private Dialogue dialogueSystem;

    [SerializeField]
    private bool taskDone = false;

    [SerializeField]
    private bool taskAccepted = false;
    private int affection = 0;
    private int todaysTask = 0;

    [SerializeField]
    private Tasks tasks;

    [SerializeField]
    private InteractableObject[] objectives;

    public void ResetTasks()
    {
        taskAccepted = false;
        taskDone = false;
        RandomizeTask();
    }

    void Awake()
    {
        float transparencyOnStart = 0f;
        ChangeTransparency(transparencyOnStart);
        ResetTasks();
    }

    private void StartTask(int index)
    {
        var objectiveList = new List<InteractableObject>(objectives);
        objectiveList.Sort((a, b) => a.GetID().CompareTo(b.GetID()));
        objectives = objectiveList.ToArray();
        objectives[index].gameObject.SetActive(true);
    }

    private void ChooseDialogue()
    {
        if (taskDone)
        {
            dialogueID = 0;
        }
        else if (taskAccepted)
        {
            dialogueID = 1;
        }
        else
        {
            dialogueID = todaysTask;
            taskAccepted = true;
        }
    }

    public void FinishTask()
    {
        taskDone = true;
        affection++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            float transparencyOnAppearance = 1f;
            ChangeTransparency(transparencyOnAppearance);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            float transparencyOnAppearance = 0f;
            ChangeTransparency(transparencyOnAppearance);
        }
    }

    //FIXME: Needs material shader that allows transparency
    private void ChangeTransparency(float transparency)
    {
        var col = this.gameObject.GetComponent<Renderer>().material.color;
        col.a = transparency;
    }

    public void InteractionStart()
    {
        ChooseDialogue();
        dialogueSystem.DialogueStart(dialogues[dialogueID].lines);
    }

    public void InteractionContinue()
    {
        dialogueSystem.NextLine();
    }

    private void RandomizeTask()
    {
        System.Random random = new System.Random();
        int taskIndex = random.Next(0, tasks.NumOfTasks() - 1);
        todaysTask = tasks.GetTaskID(taskIndex);
        StartTask(taskIndex);
    }
}
