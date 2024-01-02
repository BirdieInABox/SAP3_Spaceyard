using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string[][] lines;

    [SerializeField]
    private Dialogue dialogueSystem;

    public void InteractionStart()
    {
        dialogueSystem.DialogueStart(lines[0]);
    }

    public void InteractionContinue()
    {
        dialogueSystem.NextLine();
    }
}
