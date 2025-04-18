using UnityEngine;

public class TVInteraction : MonoBehaviour
{
    public GameObject gameStartUI;

    public void ShowGameMenu()
    {
        gameStartUI.SetActive(true);
        gameStartUI.transform.LookAt(Camera.main.transform);
        gameStartUI.transform.Rotate(0, 180f, 0);
    }
}
