using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResponseUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public PlayerDialogueDatabase playerDB;

    public void ShowOptions(string category)
    {
        foreach (Transform child in buttonParent)
            Destroy(child.gameObject);

        var entry = playerDB.entries.Find(e => e.category == category);
        if (entry == null) return;

        foreach (string line in entry.responses)
        {
            GameObject btn = Instantiate(buttonPrefab, buttonParent);
            btn.GetComponentInChildren<TMP_Text>().text = line;
            btn.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log("Player said: " + line);
                // Optional: affect NPC state here
                Hide();
            });
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
