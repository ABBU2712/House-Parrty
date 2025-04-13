using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerDialogueDatabase", menuName = "Dialogue/Player Dialogue Database")]
public class PlayerDialogueDatabase : ScriptableObject
{
    public List<PlayerDialogueEntry> entries;
}
