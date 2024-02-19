//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestGate : MonoBehaviour
{
    [SerializeField]
    private string[] lines;

    [SerializeField]
    private DialogueSystem dialogueSystem;
    private bool closed = true;

    //End the dialogue on reset
    public void EndDialogue()
    {
        dialogueSystem.EndDialogue();
    }

    //Dialogue gets started
    public void InteractionStart()
    {
        if (closed) //If gate is closed, start dialogue
            dialogueSystem.DialogueStart(lines);
        else //If not, disable this
            gameObject.SetActive(false);
    }

    //Dialogue gets continued
    public void InteractionContinue()
    {
        dialogueSystem.NextLine();
    }
}
