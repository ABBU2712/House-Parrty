using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class LightingController : MonoBehaviour
{
    public Transform reticleOrigin;            // Camera or controller
    public float rayLength = 10f;
    public Canvas uiCanvas;                    // Canvas that holds the buttons
    private GraphicRaycaster uiRaycaster;        // GraphicRaycaster on your World Space Canvas
    public EventSystem eventSystem; 
    private Button lastbtn = null;
    private Image buttonImage = null;

    // Reference to the Directional Light
    public Light directionalLight;
    public float colorChangeSpeed = 2f;

    private float hue = 0f;
    // Color presets for different "disco lights"
    public Color redColor = Color.red;
    public Color greenColor = Color.green;
    public Color blueColor = Color.blue;
    public Color yellowColor = Color.yellow;
    private bool discoEnabled;

    void Start()
    {
        // Ensure the light starts off with no color or reset color
        if (directionalLight)
        {
            directionalLight.enabled = true;  // Ensure the light is active
            directionalLight.color = Color.white;  // Default color is white
        }
        if (uiCanvas != null)
        {
            uiRaycaster = uiCanvas.GetComponent<GraphicRaycaster>();
        }
        discoEnabled = false;
    }

    void Update()
    {

        // PointerEventData pointerData = new PointerEventData(eventSystem);
        // pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2); // center of screen (or reticle target)

        Vector3 worldPoint = reticleOrigin.position + reticleOrigin.forward * 1f;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);

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

        if (directionalLight != null && discoEnabled)
        {
            // Cycle through hue values (0 to 1)
            hue += Time.deltaTime * colorChangeSpeed;
            if (hue > 1f) hue = 0f;

            // Convert hue to RGB and set it as the light color
            directionalLight.color = Color.HSVToRGB(hue, 1f, 1f);
        }

    }


    // Reset all colors by setting the light to white
    void ResetLightColor()
    {
        if (directionalLight)
        {
            directionalLight.color = Color.white;
            discoEnabled = false;
        }
    }

    // Function to activate the Red light
    public void ActivateRedLight()
    {
        ResetLightColor();
        if (directionalLight)
        {
            directionalLight.color = redColor;  // Set to red
        }
    }

    // Function to activate the Green light
    public void ActivateGreenLight()
    {
        ResetLightColor();
        if (directionalLight)
        {
            directionalLight.color = greenColor;  // Set to green
        }
    }

    // Function to activate the Blue light
    public void ActivateBlueLight()
    {
        ResetLightColor();
        if (directionalLight)
        {
            directionalLight.color = blueColor;  // Set to blue
        }
    }

    // Function to activate the Yellow light
    public void ActivateYellowLight()
    {
        ResetLightColor();
        if (directionalLight)
        {
            directionalLight.color = yellowColor;  // Set to yellow
        }
    }

    public void DiscoEnabled()
    {
        ResetLightColor();
        discoEnabled = true;
    }

    void updateColor(Button btn) 
    {
        if(lastbtn != null){
            buttonImage = lastbtn.GetComponent<Image>();
            buttonImage.color = Color.white;
        }
        buttonImage = btn.GetComponent<Image>();
        buttonImage.color = Color.yellow;
        lastbtn = btn;
    }

}
