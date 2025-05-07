using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    public int totalLaps = 10;
    private int playerLap = 0;
    private int aiLap = 0;
    private bool raceFinished = false;

    public TMP_Text resultText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (raceFinished) return;

        if (other.CompareTag("Player"))
        {
            playerLap++;
            Debug.Log("Player Lap: " + playerLap);
        }
        else if (other.CompareTag("NPC"))
        {
            aiLap++;
            Debug.Log("AI Lap: " + aiLap);
        }

        CheckWinner();
    }

    void CheckWinner()
    {
        if (playerLap >= totalLaps)
        {
            raceFinished = true;
            resultText.text = "Player Wins!";
            Time.timeScale = 0; // Pause game
        }
        else if (aiLap >= totalLaps)
        {
            raceFinished = true;
            resultText.text = "AI Wins!";
            Time.timeScale = 0; // Pause game
        }
    }
}
