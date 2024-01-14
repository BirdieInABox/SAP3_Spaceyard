using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuest : MonoBehaviour
{
    //EventManager for broadcast messages
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

    public Dialogues[] dialoguesChapter1 = new Dialogues[dialogueID];
    public Dialogues[] dialoguesChapter2 = new Dialogues[dialogueID];

    [SerializeField] //The dialogue handler
    private Dialogue dialogueSystem;

    private int chapter = 1; //How far into the main quest is the player?

    private bool taskAccepted = false; //Has the player talked to the NPC already?
    private int candlesLeft = 0; //The level of affection reached

    [SerializeField] //The list of tasks each generic NPC can trigger
    private Tasks tasks;

    [SerializeField] //Which objects are bound to this NPC for its tasks?
    private InteractableObject[] candles;

    [SerializeField]
    private PickupItem key;

    [SerializeField] //Should the NPC appear at day or at night?
    private bool isDiurnal;

    //Is it currently day?
    private bool isDay;

    void Start()
    {
        candlesLeft = candles.Length;
    }

    private void ChooseDialogue()
    {
        switch (chapter)
        {
            case 1:

                if (!taskAccepted)
                {
                    dialogueID = 0;
                    taskAccepted = true;
                }
                else
                {
                    dialogueID = (candles.Length + 1) - candlesLeft;
                }

                break;
            case 2:
                if (!taskAccepted)
                    Debug.Log("Here the next chapter would be introduced");
                break;
            default:
                break;
        }
    }

    public void InteractionStart()
    {
        ChooseDialogue();
        switch (chapter)
        {
            case 1:
                dialogueSystem.DialogueStart(dialoguesChapter1[dialogueID].lines);
                if (candlesLeft == 0)
                {
                    ShowKey();
                }

                break;
            case 2:
                dialogueSystem.DialogueStart(dialoguesChapter2[dialogueID].lines);

                break;
            default:
                break;
        }
    }

    //Dialogue gets continued
    public void InteractionContinue()
    {
        dialogueSystem.NextLine();
    }

    public void CandleLit()
    {
        candlesLeft--;
    }

    private void ShowKey()
    {
        key.gameObject.SetActive(true);
    }
}
