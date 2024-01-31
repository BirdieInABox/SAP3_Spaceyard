//Author: Kim Bolender
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //dialogue IDs
    private static int dialogueID,
        lineID;
    private string[] lines; //The NPC's raw dialogue lines

    [System.Serializable] //This NPC's dialogues
    public class Dialogues
    {
        public string[] lines = new string[lineID];
    }

    [SerializeField]
    private Journal journal;
    public Dialogues[] dialogues = new Dialogues[dialogueID];

    [SerializeField] //The dialogue handler
    private Dialogue dialogueSystem;

    private bool taskDone = false; //Has the player finished today's task?

    private bool taskAccepted = false; //Has the player talked to the NPC today without having finished today's task?
    private int affection = 0; //The level of affection reached
    private int todaysTask = 0; //The ID of today's random task

    //The list of tasks each generic NPC can trigger
    public Tasks tasks;

    [SerializeField] //Which objects are bound to this NPC for its tasks?
    private InteractableObject[] objectives;

    [SerializeField] //Should the NPC appear at day or at night?
    private bool isDiurnal;

    //Is it currently day?

    private bool isDay;

    public void StartTime(bool _isDay)
    {
        isDay = _isDay;
        OnReset();
    }

    //Reset the state of tasks and randomize a new taksID
    public void ResetTasks()
    {
        taskAccepted = false;
        taskDone = false;
        RandomizeTask();
    }

    //Called when day/night starts
    public void OnReset()
    {
        ChangeTransparency(isDiurnal == isDay);
        if (isDiurnal == isDay) //if nocturnal && is night OR diurnal && is day
            ResetTasks();
    }

    //Starts a new task with the taskID index
    private void StartTask(int index)
    {
        var objectiveList = new List<InteractableObject>(objectives);
        objectiveList.Sort((a, b) => a.GetID().CompareTo(b.GetID()));
        objectives = objectiveList.ToArray();
        objectives[index].gameObject.SetActive(true);
        objectiveList[index].gameObject.GetComponent<InteractableObject>().StartTask();
    }

    //Depending on the status of today's task, choose the dialogue that is shown next
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
            int index = tasks.GetTaskIndex(todaysTask);
            journal.AddEntry(
                this.name,
                tasks.GetTaskName(index),
                tasks.GetNeededItem(index).displayName
            );
        }
    }

    //Gets called when player finishes today's task
    public void FinishTask()
    {
        taskDone = true;
        affection++;
        journal.RemoveEntry(this.name);
    }

    //If player gets close, become visible
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            ChangeTransparency(isDiurnal == isDay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeTransparency(isDiurnal == isDay);
        }
    }

    //Changes the NPC's transparency
    private void ChangeTransparency(bool transparency)
    {
        transform.GetChild(0).gameObject.SetActive(transparency);
    }

    //Dialogue gets started
    public void InteractionStart()
    {
        if (isDay == isDiurnal)
        {
            ChooseDialogue();
            dialogueSystem.DialogueStart(dialogues[dialogueID].lines);
        }
    }

    //Dialogue gets continued
    public void InteractionContinue()
    {
        dialogueSystem.NextLine();
    }

    //randomizes today's taskID
    private void RandomizeTask()
    {
        System.Random random = new System.Random();
        int taskIndex = random.Next(0, (tasks.NumOfTasks() - 1));
        todaysTask = tasks.GetTaskID(taskIndex);

        StartTask(taskIndex);
    }
}
