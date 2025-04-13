using UnityEngine;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "NPC/Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public DialogueResponse[] allResponses;
}
