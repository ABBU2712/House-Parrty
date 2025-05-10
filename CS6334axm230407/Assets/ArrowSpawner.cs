using UnityEngine;
using TMPro;
using Assets.SuperGoalie.Scripts.Managers;

public class ArrowSpawner : MonoBehaviour
{
    public TMP_Text arrowText;
    private string currentDirection;
    private float interval = 1.5f;

    public GameManager gameManager;

    void Start()
    {
        InvokeRepeating(nameof(ShowArrow), 1f, interval);
    }

    void ShowArrow()
    {
        if (!gameManager.gameActive) return;

        currentDirection = Random.value > 0.5f ? "UP" : "DOWN";
        arrowText.text = currentDirection == "UP" ? "↑" : "↓";
        gameManager.SetExpectedDirection(currentDirection);
    }
}
