using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BillBoardScore : MonoBehaviour
{
    //public GameObject billCanvas; // Assign your settings Canvas in the Inspector
    public Transform reticleOrigin;            // Camera or controller
    public float rayLength = 10f;
    public Canvas uiCanvas;                    // Canvas that holds the buttons
    private GraphicRaycaster uiRaycaster;        // GraphicRaycaster on your World Space Canvas
    public EventSystem eventSystem; 
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

    void Update()
    {
        // Toggle with Escape key (optional)

        // PointerEventData pointerData = new PointerEventData(eventSystem);
        // pointerData.position = new Vector2(Screen.width / 2f, Screen.height / 2f); // center of screen (or reticle target)

        // Vector3 worldPoint = Camera.main.transform.position + Camera.main.transform.forward * 1f; // 1.5 units in front
        // Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);

        // PointerEventData pointerData = new PointerEventData(eventSystem);
        // pointerData.position = new Vector2(screenPoint.x, screenPoint.y);

        // List<RaycastResult> results = new List<RaycastResult>();
        // uiRaycaster.Raycast(pointerData, results);

        Vector3 start = reticleOrigin.position;
        Vector3 end = start + reticleOrigin.forward * rayLength;

        // Project world point to screen
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(end);

        // Prepare pointer data for UI raycast
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = new Vector2(screenPoint.x, screenPoint.y);

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
