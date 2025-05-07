using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NPCActionMenu : MonoBehaviour
{
    public Button xButton;
    public Button yButton;
    public PlayerDialogueDatabase playerDB;

    public void ShowPlayerResponses(string category)
    {
        PlayerDialogueEntry entry = playerDB.entries.Find(e => e.category.ToLower() == category.ToLower());

        if (entry == null || entry.responses.Length == 0)
        {
            Debug.LogWarning("No player responses found for category: " + category);
            return;
        }

        List<string> pool = new List<string>(entry.responses);
        Shuffle(pool);

        xButton.GetComponentInChildren<TMP_Text>().text = pool.Count > 0 ? pool[0] + " (X)" : "N/A";
        xButton.onClick.RemoveAllListeners();
        xButton.onClick.AddListener(() => {
            Debug.Log("Player responded (X): " + pool[0]);
            gameObject.SetActive(false);
        });

        yButton.GetComponentInChildren<TMP_Text>().text = pool.Count > 1 ? pool[1] + " (Y)" : "N/A";
        yButton.onClick.RemoveAllListeners();
        yButton.onClick.AddListener(() => {
            Debug.Log("Player responded (Y): " + pool[1]);
            gameObject.SetActive(false);
        });

        gameObject.SetActive(true);
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
