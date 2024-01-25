using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField]
    private GameObject entryPrefab;

    [SerializeField]
    private GameObject contentPanel;

    public void AddEntry(string npc, string info, string item)
    {
        GameObject newEntry = Instantiate(entryPrefab) as GameObject;
        JournalEntry entryData = newEntry.GetComponent<JournalEntry>();
        entryData.giver = npc;
        entryData.info = info;
        entryData.item = item;
        newEntry.transform.SetParent(contentPanel.transform);
        newEntry.transform.localScale = Vector3.one;
        entryData.EnterData();
    }

    public void RemoveEntry(string npc)
    {
        foreach (Transform child in contentPanel.transform)
        {
            if (child.GetComponent<JournalEntry>().giver == npc)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
}
