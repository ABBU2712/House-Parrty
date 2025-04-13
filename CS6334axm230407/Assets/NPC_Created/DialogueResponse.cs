using UnityEngine;

[System.Serializable]
public class DialogueResponse
{
    public string keyword;
    [TextArea(2, 4)] public string[] responses;
    public string mood = "default";
    public string requiredFlag = "";
}
