using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    public Transform reticleOrigin;            // Camera or controller
    public float rayLength = 10f;
    public Canvas uiCanvas;                    // Canvas that holds the buttons
    private GraphicRaycaster uiRaycaster;        // GraphicRaycaster on your World Space Canvas
    public EventSystem eventSystem; 
    private Button lastbtn = null;
    private Image buttonImage = null;

    private bool isMenuOpen = false;

    void Start()
    {
        if (uiCanvas != null)
        {
            uiRaycaster = uiCanvas.GetComponent<GraphicRaycaster>();
        }

        if (uiRaycaster == null)
        {
            Debug.LogError("GraphicRaycaster not found on assigned Canvas!");
        }

        if (eventSystem == null)
        {
            Debug.LogError("EventSystem is not assigned!");
        }
        if (EventSystem.current == null) {
            Debug.LogWarning("EventSystem.current is null. Make sure an EventSystem exists in the scene.");
        }
    }

    void Update()
    {
        // Toggle with Escape key (optional)
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("js10"))
        {
            ToggleSettings();
        }

        Vector3 worldPoint = reticleOrigin.position + reticleOrigin.forward * 1f;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);

        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = new Vector2(screenPoint.x, screenPoint.y);

        // PointerEventData pointerData = new PointerEventData(eventSystem);
        // pointerData.position = new Vector2(Screen.width / 2f, Screen.height / 2f); // center of screen (or reticle target)

        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            Debug.Log(button);
            if(button != null){
                if(button != lastbtn){
                    updateColor(button);
                }

                if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("js2"))
                {
                    button.onClick.Invoke(); // simulate a click!
                    Debug.Log("UI Button clicked: " + button.name);
                }
            }
        }

    }

    public void ToggleSettings()
    {
        isMenuOpen = !isMenuOpen;
        uiCanvas.gameObject.SetActive(isMenuOpen);
    }

    public void Resume()
    {
        Debug.Log("resume game");
        ToggleSettings();
    }

    void updateColor(Button btn) {
        if(lastbtn != null){
            buttonImage = lastbtn.GetComponent<Image>();
            buttonImage.color = Color.white;
        }
        buttonImage = btn.GetComponent<Image>();
        buttonImage.color = Color.yellow;
        lastbtn = btn;
    }
}
