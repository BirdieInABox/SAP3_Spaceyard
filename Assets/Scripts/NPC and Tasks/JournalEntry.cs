using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalEntry : MonoBehaviour
{
    [SerializeField]
    private TMP_Text giverText;

    [SerializeField]
    private TMP_Text infoText;

    [SerializeField]
    private TMP_Text itemText;

    public string giver;
    public string info;
    public string item;

    public void EnterData()
    {
        giverText.SetText(giver);
        infoText.SetText(info);
        itemText.SetText(item);
    }
}
