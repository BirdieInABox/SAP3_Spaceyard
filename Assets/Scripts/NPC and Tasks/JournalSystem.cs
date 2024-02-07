//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject entryPrefab;

    [SerializeField]
    private GameObject contentPanel;

    //Instantiates an entry and fills it with information
    public void AddEntry(string npc, string info, string item)
    {
        //Instantiates an entry
        GameObject newEntry = Instantiate(entryPrefab) as GameObject;
        //Get the journal entry's data
        JournalEntry entryData = newEntry.GetComponent<JournalEntry>();
        //Set quest giver name
        entryData.giver = npc;
        //Set quest text
        entryData.info = info;
        //Set name of needed item
        entryData.item = item;
        //Set the content panel as parent of entry
        newEntry.transform.SetParent(contentPanel.transform);
        //Manually reset scale to 1
        newEntry.transform.localScale = Vector3.one;
        entryData.EnterData();
    }

    //Removes an entry, filtered by the questgiver
    public void RemoveEntry(string npc)
    {
        //Go through all children
        foreach (Transform child in contentPanel.transform)
        {
            //if quest giver == npc
            if (child.GetComponent<JournalEntry>().giver == npc)
            {
                //remove entry
                Destroy(child.gameObject);
                break;
            }
        }
    }
}
