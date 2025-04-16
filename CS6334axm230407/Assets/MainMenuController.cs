using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject menuPanel;         // Assign your menu panel GameObject
    public Button[] menuButtons;         // Assign all your buttons (in order)

    [Header("Scene References")]
    public AudioSource musicSource;      // Your background music AudioSource
    public Light sceneLight;             // Main scene light (e.g. Directional Light)

    private bool isMenuOpen = false;
    private int currentIndex = 0;

    void Start()
    {
        menuPanel.SetActive(false);      // Menu starts hidden
        Time.timeScale = 1f;             // Game runs normally at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }

        if (isMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.X)) Navigate(-1); // Up
            if (Input.GetKeyDown(KeyCode.B)) Navigate(1);  // Down

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("js0"))
            {
                menuButtons[currentIndex].onClick.Invoke(); // Select current
            }
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
        Time.timeScale = isMenuOpen ? 0f : 1f;

        if (isMenuOpen)
        {
            currentIndex = 0;
            HighlightButton(currentIndex);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void Navigate(int direction)
    {
        currentIndex += direction;

        if (currentIndex < 0)
            currentIndex = menuButtons.Length - 1;
        else if (currentIndex >= menuButtons.Length)
            currentIndex = 0;

        HighlightButton(currentIndex);
    }

    void HighlightButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(menuButtons[index].gameObject);
        menuButtons[index].Select();
    }

    //  Toggle Music (with TextMeshPro update)
    public void ToggleMusic()
    {
        if (musicSource == null) return;

        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }

        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected != null)
        {
            TMP_Text text = selected.GetComponentInChildren<TMP_Text>();
            if (text != null)
                text.text = musicSource.isPlaying ? "Music (on)" : "Music (off)";
        }
    }

    // 
    public void ToggleLighting()
    {
        if (sceneLight != null)
            sceneLight.enabled = !sceneLight.enabled;
    }

    
    public void ResumeGame()
    {
        ToggleMenu(); // Just closes the menu
    }
}
