using UnityEngine;
using UnityEngine.UI;

public class DJBoxUIManager : MonoBehaviour
{
    public GameObject menuPanel;
    public AudioClip[] songs;
    public AudioSource audioSource;
    public Button songButtonPrefab;
    public Transform buttonContainer;

    private void Start()
    {
        PopulateMenu();
        menuPanel.SetActive(false);
    }

    private void PopulateMenu()
    {
        foreach (AudioClip song in songs)
        {
            Button btn = Instantiate(songButtonPrefab, buttonContainer);
            btn.GetComponentInChildren<Text>().text = song.name;

            btn.onClick.AddListener(() => {
                audioSource.clip = song;
                audioSource.Play();
            });
        }
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }
}
