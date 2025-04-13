using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCActionMenu : MonoBehaviour
{
    public Button xButton;
    public Button yButton;
    public PlayerDialogueDatabase playerDB;

    private SimpleNPC npc;

    public void Setup(SimpleNPC npcScript)
    {
        npc = npcScript;
    }

    public void ShowPlayerResponses(string category)
    {
        var entry = playerDB.entries.Find(e => e.category == category);
        if (entry == null) return;

        if (entry.responses.Length > 0)
        {
            string responseX = entry.responses[Random.Range(0, entry.responses.Length)];
            xButton.GetComponentInChildren<TMP_Text>().text = responseX + " (X)";
            xButton.onClick.RemoveAllListeners();
            xButton.onClick.AddListener(() => {
                Debug.Log("Player responded: " + responseX);
                gameObject.SetActive(false);
            });
        }

        if (entry.responses.Length > 1)
        {
            string responseY = entry.responses[Random.Range(0, entry.responses.Length)];
            yButton.GetComponentInChildren<TMP_Text>().text = responseY + " (Y)";
            yButton.onClick.RemoveAllListeners();
            yButton.onClick.AddListener(() => {
                Debug.Log("Player responded: " + responseY);
                gameObject.SetActive(false);
            });
        }

        gameObject.SetActive(true);
    }

}
