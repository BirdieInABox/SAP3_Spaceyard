//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //The text-field of the dialogue window
    [SerializeField]
    private TextMeshProUGUI textContent;

    [SerializeField]
    private Player player;

    //The different texts of the dialogue, in order of appearance

    private string[] lines;

    //The speed in which the characters of the text appear
    [SerializeField]
    private float textSpeed;

    //the index of the current text passage of the dialogue
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //Empty the text-field
        textContent.text = string.Empty;
        //REMOVEME: Debugging:
        // DialogueStart(null);
    }

    // Update is called once per frame
    void Update()
    {
        //Add a button-down event if you do not use the new unity InputSystem and
        //you do not call "DialogueStart()" and "NextLine()" from another script.
    }

    //Start first instance of this dialogue
    //Malte, remove the parameter (Everything in the "(...)")
    public void DialogueStart(string[] npcDialogue)
    {
        player.toggleDialogue();
        gameObject.SetActive(true);
        textContent.text = string.Empty;
        //Malte, remove this line
        lines = npcDialogue;
        //Reset index to first text passage
        index = 0;
        //Write first line
        StartCoroutine(TypeLine());
    }

    //Writes lines one letter at a time, dependent on the textSpeed variable
    IEnumerator TypeLine()
    {
        //For each character in the current text passage
        foreach (char c in lines[index].ToCharArray())
        {
            //Add character to text-field
            textContent.text += c;
            //return and wait according to the textSpeed
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //Goes to the next passage-
    public void NextLine()
    {
        //if the entire text passage is currently being shown in the text-field
        if (textContent.text == lines[index])
        {
            //If the end of the dialogue hasn't been reached yet
            if (index < lines.Length - 1)
            {
                //Set index to next passage
                index++;
                //Empty the text-field
                textContent.text = string.Empty;
                //Write the next line
                StartCoroutine(TypeLine());
            }
            else //If the end of the dialogue (last passage) has been reached
            {
                //Malte, remove this line
                lines = null;
                player.toggleDialogue();
                //Hide the dialogue window
                gameObject.SetActive(false);
            }
        }
        else //If the current text passage is currently NOT being fully shown in the text-field
        {
            //Skip the letter-by letter text
            StopAllCoroutines();
            //Show the current text passage instantly
            textContent.text = lines[index];
        }
    }
}
