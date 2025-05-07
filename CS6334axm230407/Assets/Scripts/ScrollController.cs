using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ScrollController : MonoBehaviour
{
    // public ScrollRect scrollRect;
    // public float scrollStep = 0.1f; // Amount to scroll (0-1)
    public Transform reticleOrigin;            // Camera or controller
    public float rayLength = 10f;
    public Canvas uiCanvas;                    // Canvas that holds the buttons
    private GraphicRaycaster uiRaycaster;        // GraphicRaycaster on your World Space Canvas
    public EventSystem eventSystem; 
    public AudioManager audioManager;
    private Button lastbtn = null;
    private Image buttonImage = null;
    
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

    public void Update()
    {
        Vector3 worldPoint = Camera.main.transform.position + Camera.main.transform.forward * 1f; // 1.5 units in front
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);

        // 3. Use this screen point in the pointer event
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = new Vector2(screenPoint.x, screenPoint.y);

        // PointerEventData pointerData = new PointerEventData(eventSystem);
        // pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2); // center of screen (or reticle target)


        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit: " + result.gameObject.name);
            Button button = result.gameObject.GetComponent<Button>();
            Debug.Log(button);
            if(button != null){
                if(button != lastbtn){
                    updateColor(button);
                }

                if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("js2"))
                {
                    button.onClick.Invoke(); // simulate a click!
                    string buttonText = button.GetComponentInChildren<TMP_Text>().text;
                    audioManager.ChangeSong(buttonText);
                    Debug.Log("UI Button clicked: " + button.name);
                }
            }
        }
    }

    // public void ScrollUp()
    // {
    //     scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition + scrollStep);
    // }

    // public void ScrollDown()
    // {
    //     scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition - scrollStep);
    // }

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
