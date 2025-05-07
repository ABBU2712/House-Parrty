using System.Collections;
using UnityEngine;
using TMPro;

public class RaceCountdown : MonoBehaviour
{
    public TMP_Text countdownText;
    public TMP_Text playerLapText;
    public TMP_Text aiLapText;

    public GameObject playerCar;
    public GameObject aiCar;

    public int totalLaps = 10;
    private int playerLap = 1;
    private int aiLap = 1;

    private bool raceStarted = false;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // Disable car movement at start
        playerCar.SetActive(false);
        aiCar.SetActive(false);

        int count = 5;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        // Enable movement
        playerCar.SetActive(true);
        aiCar.SetActive(true);
        raceStarted = true;

        UpdateLapUI();
    }

    public void RegisterLap(string tag)
    {
        if (!raceStarted) return;

        if (tag == "Player")
            playerLap++;
        else if (tag == "NPC")
            aiLap++;

        UpdateLapUI();
    }

    void UpdateLapUI()
    {
        playerLapText.text = $"Player: {playerLap} / {totalLaps}";
        aiLapText.text = $"AI: {aiLap} / {totalLaps}";
    }
}
