//Author: Kim Bolender
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //EventManager for broadcast messages
    //FIXME: MO: Creation of a new EventManager Instance
    public EventManager<GlobalEvent> globalEventManager = new EventManager<GlobalEvent>();

    //dialogue IDs
    private static int dialogueID,
        lineID;
    private string[] lines; //The NPC's raw dialogue lines

    [System.Serializable] //This NPC's dialogues
    public class Dialogues
    {
        public string[] lines = new string[lineID];
    }

    public Dialogues[] dialogues = new Dialogues[dialogueID];

    [SerializeField] //The dialogue handler
    private Dialogue dialogueSystem;

    private bool taskDone = false; //Has the player finished today's task?

    private bool taskAccepted = false; //Has the player talked to the NPC today without having finished today's task?
    private int affection = 0; //The level of affection reached
    private int todaysTask = 0; //The ID of today's random task

    [SerializeField] //The list of tasks each generic NPC can trigger
    private Tasks tasks;

    [SerializeField] //Which objects are bound to this NPC for its tasks?
    private InteractableObject[] objectives;

    [SerializeField] //Should the NPC appear at day or at night?
    private bool isDiurnal;

    [SerializeField]
    private Clock clock;

    //Is it currently day?

    private bool isDay;

    private void Start()
    {
        globalEventManager.MarkEventAsPersistent(GlobalEvent.StartTime);
        //FIXME: MO: Add a listener that triggers OnReset() on StartTime being broadcasted
        globalEventManager.AddListener<bool>(GlobalEvent.StartTime, OnReset);

        //OnReset(clock.isDay);
    }

    //Reset the state of tasks and randomize a new taksID
    public void ResetTasks()
    {
        taskAccepted = false;
        taskDone = false;
        RandomizeTask();
    }

    //Called when day/night starts
    public void OnReset(bool isDay)
    {
        Debug.Log("Good Morning!"); //TODO: Remove me
        float transparencyOnStart = 1f; //FIXME: Fix transparency
        ChangeTransparency(transparencyOnStart);
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
    }

    //Sorts the array of objectives to mirror the array of tasks
    private void SortTasks() { }

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
        }
    }

    //Gets called when player finishes today's task
    public void FinishTask()
    {
        taskDone = true;
        affection++;
    }

    //If player gets close, become visible
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && isDiurnal == clock.isDay)
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
    //TODO: Set active/inactive
    //Changes the NPC's transparency
    private void ChangeTransparency(float transparency)
    {
        // var col = this.gameObject.GetComponent<Renderer>().material.color;
        // col.a = transparency;
        // gameObject.SetActive(transparency == 1);
    }

    //Dialogue gets started
    public void InteractionStart()
    {
        ChooseDialogue();
        dialogueSystem.DialogueStart(dialogues[dialogueID].lines);
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
