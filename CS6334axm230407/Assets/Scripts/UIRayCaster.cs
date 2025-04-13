using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections; // ✅ Required for IEnumerator (non-generic)


public class UIRayCaster : MonoBehaviour
{
    private GraphicRaycaster raycaster; // Assign this manually in Inspector
    public float raycastLength;
    private Ray ray;
    private EventSystem eventSystem;
    public GameObject cam;
    public GameObject characterObject; 
    public Transform settingsMenu;

    private Transform currentMenu = null;
    private bool isMenuVisible = false;
    private Transform cameraTransform;
    private bool isPointingMenu = false;
    private GameObject selected;
    private GameObject lastSelectedButton = null;
    private Color defaultColor = Color.white;
    private Color selectedColor = Color.yellow;
    private Vector3 rayOrigin;
    private Vector3 direction;
    private GrabObject grabObject; 
    private CharacterMovement movementScript;
    private float currentCharacterSpeed;

   void Start()
    {
        cameraTransform = transform;
        raycastLength = 10f;
        eventSystem = GameObject.FindFirstObjectByType<EventSystem>();
        grabObject = GetComponent<GrabObject>();
        movementScript = characterObject.GetComponent<CharacterMovement>();
        currentCharacterSpeed = movementScript.speed;
        currentMenu = settingsMenu;
    }

    void Update()
    {
        if (cam == null || eventSystem == null)
        {
            Debug.Log(cam);
            Debug.Log(eventSystem);
            Debug.LogWarning("Missing required references!");
            return;
        }

        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("X Pressed ");
            if(currentMenu == null) {
                Debug.Log("currentMenu not found");
            }
            if (currentMenu != null)
            {
                raycaster = currentMenu.GetComponent<GraphicRaycaster>();
                currentMenu.gameObject.SetActive(true);
                isMenuVisible = true;
                movementScript.speed = 0f;
                Debug.Log("Showing menu for ");
            }
        }

        UiRayCasterUpdate();


    }

    void UiRayCasterUpdate() {

        if(raycaster == null) return;

        rayOrigin = grabObject.getRayOrigin();
        direction = grabObject.getDirection();

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(rayOrigin + direction); // point slightly in front

        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = new Vector2(screenPoint.x, screenPoint.y);

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        Debug.DrawRay(cam.transform.position, cam.transform.forward * 10, Color.green);

        if(results.Count == 0){
            selected = null;
            //lastSelectedButton = null;
        }

        foreach (var result in results)
        {
            selected = result.gameObject;
            Debug.Log("this is selected: " + selected);
            isPointingMenu = true;
        
        }

        ChangeBackgroundColorButton();

    }


    public void HideCurrentMenu()
    {
        if (isMenuVisible && currentMenu != null)
        {
            currentMenu.gameObject.SetActive(false);
            Debug.Log("Hiding menu for ");
        }
        //Debug.Log("Hiding current menu");

        if (selected != null)
        {
            var image = selected.GetComponent<Image>();
            if (image != null)
                image.color = defaultColor;
        }

        currentMenu = null;
        isMenuVisible = false;
        selected = null;
        lastSelectedButton = null;
    }

    void ChangeBackgroundColorButton() {

        if (selected != null)
        {
            Debug.Log("Currently selected: " + selected.name);
        }
        else {
            if(lastSelectedButton != null) {
                var image = lastSelectedButton.GetComponent<Image>();
                if (image != null)
                    image.color = defaultColor;
            }
        }


        if (selected != lastSelectedButton)
        {
            // Reset previous button color
            if (lastSelectedButton != null)
            {
                var image = lastSelectedButton.GetComponent<Image>();
                if (image != null)
                    image.color = defaultColor;
            }

            // Highlight current button
            if (selected != null)
            {
                var image = selected.GetComponent<Image>();
                if (image != null)
                    image.color = selectedColor;
            }

            lastSelectedButton = selected;
        }

    }


}
