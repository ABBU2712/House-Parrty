using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlayerDialogueDatabaseFiller : EditorWindow
{
    PlayerDialogueDatabase db;

    [MenuItem("Tools/Auto-Fill Player Dialogue Database")]
    public static void ShowWindow()
    {
        GetWindow<PlayerDialogueDatabaseFiller>("Auto-Fill Player Dialogue DB");
    }

    void OnGUI()
    {
        db = (PlayerDialogueDatabase)EditorGUILayout.ObjectField("Dialogue Database", db, typeof(PlayerDialogueDatabase), false);

        if (GUILayout.Button("Generate Responses"))
        {
            if (db != null)
            {
                GenerateMockResponses(db);
                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();
                Debug.Log("Dialogue DB filled successfully!");
            }
            else
            {
                Debug.LogWarning("Assign a PlayerDialogueDatabase first.");
            }
        }
    }

    void GenerateMockResponses(PlayerDialogueDatabase db)
    {
        db.entries = new List<PlayerDialogueEntry>();

        AddEntry(db, "hello", "cheerful", new string[] {
            "Haha, thanks for inviting me!",
            "Great party vibe already!",
            "Wasn't expecting to see you here.",
            "What's the occasion?"
        });

        AddEntry(db, "dance", "cheerful", new string[] {
            "Let’s groove together!",
            "These moves? I’ve been practicing.",
            "Teach me that step again!",
            "Time for a dance battle?"
        });

        AddEntry(db, "flirt", "sassy", new string[] {
            "Smooth move, party host.",
            "Don’t get distracted now 😉",
            "You're cute when you dance.",
            "Careful — I might flirt back."
        });

        AddEntry(db, "bye", "neutral", new string[] {
            "Leaving already?",
            "Catch you later!",
            "Don’t forget to grab cake on your way out.",
            "Come back if the night gets boring."
        });
    }

    void AddEntry(PlayerDialogueDatabase db, string category, string mood, string[] lines)
    {
        PlayerDialogueEntry entry = new PlayerDialogueEntry();
        entry.category = category;
        entry.mood = mood;
        entry.responses = lines;
        db.entries.Add(entry);
    }
}
