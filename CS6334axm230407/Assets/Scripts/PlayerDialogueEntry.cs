[System.Serializable]
public class PlayerDialogueEntry
{
    public string category;      // e.g., "hello", "dance"
    public string mood;          // optional (e.g., cheerful, sassy)
    public string[] responses;   // list of things the player can say
}
