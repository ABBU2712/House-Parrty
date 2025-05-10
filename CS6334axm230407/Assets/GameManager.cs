using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text gameOverText;
    private float timer;
    public bool gameActive = true;
    private string expectedDirection;

    void Update()
    {
        if (!gameActive) return;

        timer += Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(timer);

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            CheckInput("UP");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            CheckInput("DOWN");
        }
    }

    public void SetExpectedDirection(string dir)
    {
        expectedDirection = dir;
    }

    void CheckInput(string playerInput)
    {
        if (playerInput != expectedDirection)
        {
            gameOverText.text = "Game Over!\nFinal Score: " + Mathf.FloorToInt(timer);
            gameActive = false;
        }
    }
}
