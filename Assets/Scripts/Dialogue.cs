using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textContent;

    [SerializeField]
    private string[] lines;

    [SerializeField]
    private float textSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textContent.text = string.Empty;
        DialogueStart(null);
    }

    // Update is called once per frame
    void Update() { }

    public void DialogueStart(string[] npcDialogue)
    {
        lines = npcDialogue;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textContent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (textContent.text == lines[index])
        {
            if (index < lines.Length - 1)
            {
                index++;
                textContent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                lines = null;
                gameObject.SetActive(false);
            }
        }
        else
        {
            StopAllCoroutines();
            textContent.text = lines[index];
        }
    }
}
