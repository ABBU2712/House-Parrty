using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ObjectMenuHandler : MonoBehaviour
{
    public Canvas objectMenuCanvas;
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    public SettingsMenuController settingsController;
    public Transform headTransform;

    private PointerEventData pointerData;
    private GameObject currentHovered;
    private GameObject objectToActOn;
    private bool isMenuActive = false;

    public void ShowMenu(GameObject target)
    {
        objectToActOn = target;
        objectMenuCanvas.gameObject.SetActive(true);
        isMenuActive = true;
    }

    public void CloseMenu()
    {
        objectMenuCanvas.gameObject.SetActive(false);
        isMenuActive = false;
        currentHovered = null;
    }

    void Update()
    {
        if (!isMenuActive) return;

        Debug.Log("Update method is hit");
        pointerData = new PointerEventData(eventSystem);
        Vector3 screenCenter = Camera.main.WorldToScreenPoint(headTransform.position + headTransform.forward * 2f);
        pointerData.position = new Vector2(screenCenter.x, screenCenter.y);

        var results = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        ResetButtonHighlights();

        if (results.Count > 0)
        {
            GameObject hitUI = results[0].gameObject;
            HighlightButton(hitUI);
            currentHovered = hitUI;

            if (Input.GetButtonDown("js1"))
            {
                Debug.Log("JS 1 is hit");
                if (hitUI.CompareTag("Grab"))
                {
                    GrabObject();
                }
                else if (hitUI.CompareTag("Store"))
                {
                    StoreObject();
                }
                else if (hitUI.CompareTag("Exit"))
                {
                    CloseMenu();
                }
            }
        }
    }

    void HighlightButton(GameObject btn)
    {
        var button = btn.GetComponent<Button>();
        if (button != null)
        {
            var colors = button.colors;
            colors.normalColor = Color.yellow;
            button.colors = colors;
        }
    }

    void ResetButtonHighlights()
    {
        foreach (Button btn in objectMenuCanvas.GetComponentsInChildren<Button>())
        {
            var colors = btn.colors;
            colors.normalColor = Color.white;
            btn.colors = colors;
        }
    }

    void GrabObject()
    {
        objectToActOn.transform.SetParent(Camera.main.transform);
        objectToActOn.transform.localPosition = new Vector3(0, 0, 1f);
        settingsController.raycastController.SetHeldObject(objectToActOn);
        CloseMenu();
    }

    void StoreObject()
    {
        if (settingsController.CanStoreMore())
        {
            settingsController.StoreItem(objectToActOn);
            objectToActOn.SetActive(false);
        }
        else
        {
            settingsController.ShowInventoryFullMessage();
        }

        CloseMenu();
    }
}
