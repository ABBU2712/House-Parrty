using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public RaceCountdown raceManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            raceManager.RegisterLap(other.tag);
        }
    }
}
