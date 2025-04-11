using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("UI Reference")]
    public TMP_Text toggleButtonText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateButtonText();
    }

    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }

        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        if (toggleButtonText != null)
        {
            toggleButtonText.text = audioSource.isPlaying ? "Stop Music" : "Play Music";
        }
    }
}
