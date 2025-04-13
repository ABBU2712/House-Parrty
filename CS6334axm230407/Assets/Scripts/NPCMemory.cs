using System.Collections.Generic;
using UnityEngine;

public class NPCMemory : MonoBehaviour
{
    public List<string> memoryFlags = new List<string>();

    public void Remember(string flag)
    {
        if (!memoryFlags.Contains(flag))
        {
            memoryFlags.Add(flag);
            Debug.Log($"NPC remembered: {flag}");
        }
    }

    public bool Knows(string flag)
    {
        return memoryFlags.Contains(flag);
    }
}
