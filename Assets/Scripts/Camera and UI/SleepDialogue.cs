using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SleepDialogue : MonoBehaviour
{
    private string[] lines;

    [System.Serializable] //This NPC's dialogues
    public class Dialogues
    {
        public string[] lines;
    }

    [SerializeField]
    private PlayerController player;

    public Dialogues[] dialogues;

    [SerializeField]
    private TMP_Text textContent;

    [SerializeField]
    private DialogueSystem dialogueSystem;

    public void DialogueStart(bool oversleep, bool isNight)
    {
        ToggleCursor();
        player.toggleDialogue();
        gameObject.SetActive(true);
        textContent.text = string.Empty;

        int index;
        if (oversleep)
        {
            index = 0;
        }
        else if (isNight)
        {
            index = 1;
        }
        else
        {
            index = 2;
        }
        lines = dialogues[index].lines;
        //Write first line
        StartCoroutine(TypeLine());
    }

    //Writes lines one letter at a time, dependent on the textSpeed variable
    IEnumerator TypeLine()
    {
        foreach (string line in lines)
        { //For each character in the current text passage
            foreach (char c in line.ToCharArray())
            {
                //Add character to text-field
                textContent.text += c;
                //return and wait according to the textSpeed
                yield return new WaitForSeconds(dialogueSystem.GetSpeed());
            }
            textContent.text += "\n";
        }
    }

    public void EndDialogue()
    {
        textContent.SetText(string.Empty);
        gameObject.SetActive(false);
        player.toggleDialogue();
        ToggleCursor();
    }

    //Turns on/off cursor and lock/unlocks it
    private void ToggleCursor()
    {
        //unlock if locked
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else //lock if unlocked
            Cursor.lockState = CursorLockMode.Locked;
        //toggle cursor visibility
        Cursor.visible = !Cursor.visible;
    }
}
