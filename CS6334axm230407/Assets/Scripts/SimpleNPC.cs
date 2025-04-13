using UnityEngine;
using TMPro;
using System.Collections;

public class SimpleNPC : MonoBehaviour
{
    public DialogueDatabase database;
    public NPCMemory memory;
    public string npcMood = "default";

    public void RespondTo(string playerInput)
    {
        Debug.Log("Input: " + playerInput + " | Mood: " + npcMood);

        foreach (var response in database.allResponses)
        {
            Debug.Log($"🔍 Checking: keyword='{response.keyword}', mood='{response.mood}', requiredFlag='{response.requiredFlag}'");

            if (!string.Equals(playerInput, response.keyword, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"❌ Keyword mismatch: input='{playerInput}' ≠ keyword='{response.keyword}'");
                continue;
            }

            if (response.mood != npcMood)
            {
                Debug.Log($"❌ Mood mismatch: npcMood='{npcMood}' ≠ response.mood='{response.mood}'");
                continue;
            }

            if (!string.IsNullOrEmpty(response.requiredFlag) && !memory.Knows(response.requiredFlag))
            {
                Debug.Log($"❌ Missing required flag: '{response.requiredFlag}' not in memory");
                continue;
            }

            if (response.responses == null || response.responses.Length == 0)
            {
                Debug.Log("❌ Response list is empty");
                ShowResponse("...");
                return;
            }

            string chosen = response.responses[Random.Range(0, response.responses.Length)];
            Debug.Log("✅ MATCHED → " + chosen);
            ShowResponse(chosen);
            NPCActionMenu menu = GetComponentInChildren<NPCActionMenu>();
            if (menu != null)
            {
                Debug.Log("Menu should be shown");
                menu.ShowPlayerResponses(playerInput);
            }
            else
            {
                Debug.Log("Menu not shown shown");
            }

            return;
        }

        Debug.Log("NPC: ... (no match)");
        ShowResponse("...");
    }



    public void ShowResponse(string line)
    {
        Transform bubble = transform.Find("ChatBubble");

        if (bubble == null)
            return;

        bubble.gameObject.SetActive(true);

        TMP_Text text = bubble.GetComponentInChildren<TMP_Text>();
        if (text != null)
            text.text = line;

        StartCoroutine(HideAfterSeconds(bubble.gameObject, 3f));
    }

    IEnumerator HideAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
