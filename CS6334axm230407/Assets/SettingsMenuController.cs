using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsMenuController : MonoBehaviour
{
    private Button[] menuButtons;
    public GameObject objectMenu;
    public GameObject raycastAndMovementObject;
    public RaycastController raycastController;
    public CharacterMovement characterMovement;

    private int currentIndex = 0;
    private bool inputLocked = false;
    private Transform settingsCanvas;
    private bool menuIsActive = false;
    private float[] rayLengths = new float[] { 1f, 10f, 50f };
    private string[] rayLabels = new string[] { "1m", "10m", "50m" };
    private int raycastIndex = 1;
    private string[] speedLabels = new string[] { "Low", "Medium", "High" };
    private int[] speedValues = new int[] { 5, 10, 20 };
    private int speedIndex = 0;
    private float previousSpeed = 0f;

    public GameObject inventoryPanel;
    public Image[] inventorySlots;
    public List<Sprite> storedItems = new();
    public int maxInventory = 3;
    private int selectedInventoryIndex = 0;

    void Start()
    {
        settingsCanvas = transform.Find("Canvas");

        if (settingsCanvas != null)
        {
            settingsCanvas.gameObject.SetActive(false);
            menuButtons = new Button[]
            {
                settingsCanvas.Find("Resume").GetComponent<Button>(),
                settingsCanvas.Find("Raycast Length").GetComponent<Button>(),
                settingsCanvas.Find("Inventory").GetComponent<Button>(),
                settingsCanvas.Find("Speed").GetComponent<Button>(),
                settingsCanvas.Find("Quit").GetComponent<Button>()
            };
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("js7"))
        {
            menuIsActive = !menuIsActive;
            if (settingsCanvas != null)
            {
                settingsCanvas.gameObject.SetActive(menuIsActive);
                if (menuIsActive) OnMenuOpened();
                else OnMenuClosed();
            }
        }

        if (inventoryPanel.activeSelf)
        {
            float vertical = Input.GetAxis("Vertical");

            if (!inputLocked)
            {
                if (vertical > 0.5f)
                {
                    selectedInventoryIndex = Mathf.Max(0, selectedInventoryIndex - 1);
                    HighlightInventoryItem();
                    LockInputTemporarily();
                }
                else if (vertical < -0.5f)
                {
                    selectedInventoryIndex = Mathf.Min(inventorySlots.Length - 1, selectedInventoryIndex + 1);
                    HighlightInventoryItem();
                    LockInputTemporarily();
                }
            }

            if (Input.GetButtonDown("js5"))
            {
                GrabFromInventory();
            }

            return;
        }

        if (!menuIsActive) return;

        float move = Input.GetAxis("Vertical");
        if (!inputLocked)
        {
            if (move > 0.5f)
            {
                currentIndex = Mathf.Max(0, currentIndex - 1);
                HighlightCurrent();
                LockInputTemporarily();
            }
            else if (move < -0.5f)
            {
                currentIndex = Mathf.Min(menuButtons.Length - 1, currentIndex + 1);
                HighlightCurrent();
                LockInputTemporarily();
            }
        }

        if (Input.GetButtonDown("js1"))
        {
            HandleSelection(currentIndex);
        }
    }

    void OnMenuOpened()
    {
        currentIndex = 0;
        HighlightCurrent();

        if (characterMovement != null)
        {
            previousSpeed = characterMovement.speed;
            characterMovement.speed = 0f;
        }

        if (raycastAndMovementObject != null)
            raycastAndMovementObject.SetActive(false);

        if (objectMenu != null)
            objectMenu.SetActive(false);
    }

    void OnMenuClosed()
    {
        if (raycastAndMovementObject != null)
            raycastAndMovementObject.SetActive(true);
    }

    void HighlightCurrent()
    {
        if (menuButtons == null || menuButtons.Length == 0) return;

        currentIndex = Mathf.Clamp(currentIndex, 0, menuButtons.Length - 1);
        for (int i = 0; i < menuButtons.Length; i++)
        {
            var colors = menuButtons[i].colors;
            colors.normalColor = (i == currentIndex) ? Color.yellow : Color.white;
            menuButtons[i].colors = colors;
        }
    }

    void LockInputTemporarily()
    {
        inputLocked = true;
        Invoke(nameof(UnlockInput), 0.25f);
    }

    void UnlockInput()
    {
        inputLocked = false;
    }

    void Resumegame()
    {
        settingsCanvas.gameObject.SetActive(false);
        inventoryPanel.SetActive(false);
        menuIsActive = false;

        if (characterMovement != null)
            characterMovement.speed = previousSpeed;

        if (raycastAndMovementObject != null)
            raycastAndMovementObject.SetActive(true);
    }

    void ToggleRaycastLength()
    {
        raycastIndex = (raycastIndex + 1) % rayLengths.Length;

        if (raycastController != null)
            raycastController.rayLength = rayLengths[raycastIndex];

        TMP_Text buttonText = menuButtons[1].GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
            buttonText.text = "Raycast Length: " + rayLabels[raycastIndex];
    }

    void ToggleSpeed()
    {
        speedIndex = (speedIndex + 1) % speedValues.Length;

        if (characterMovement != null)
            characterMovement.speed = speedValues[speedIndex];

        TMP_Text buttonText = menuButtons[3].GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
            buttonText.text = "Speed: " + speedLabels[speedIndex];
    }

    void ToggleInventory()
    {
        settingsCanvas.gameObject.SetActive(false);
        inventoryPanel.SetActive(true);
        selectedInventoryIndex = 0;
        HighlightInventoryItem();
    }

    void HighlightInventoryItem()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].color = (i == selectedInventoryIndex) ? Color.yellow : Color.white;
        }
    }

    void GrabFromInventory()
    {
        Sprite selectedSprite = inventorySlots[selectedInventoryIndex].sprite;

        if (selectedSprite != null)
        {
            GameObject held = new GameObject("HeldObject");
            held.transform.SetParent(Camera.main.transform);
            held.transform.localPosition = new Vector3(0, 0, 1f);

            SpriteRenderer sr = held.AddComponent<SpriteRenderer>();
            sr.sprite = selectedSprite;

            inventorySlots[selectedInventoryIndex].sprite = null;
            inventorySlots[selectedInventoryIndex].color = Color.white;
            storedItems.RemoveAt(selectedInventoryIndex);

            Resumegame();
        }
    }

    public bool CanStoreMore()
    {
        return storedItems.Count < maxInventory;
    }

    public void StoreItem(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
        if (sr != null && CanStoreMore())
        {
            storedItems.Add(sr.sprite);
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].sprite == null)
                {
                    inventorySlots[i].sprite = sr.sprite;
                    break;
                }
            }
        }
    }

    public void ShowInventoryFullMessage()
    {
        StartCoroutine(ShowTempMessage("Inventory is Full!", 2f));
    }

    System.Collections.IEnumerator ShowTempMessage(string msg, float delay)
    {
        Debug.Log(msg);
        yield return new WaitForSeconds(delay);
    }

    void HandleSelection(int index)
    {
        switch (index)
        {
            case 0: Resumegame(); break;
            case 1: ToggleRaycastLength(); break;
            case 2: ToggleInventory(); break;
            case 3: ToggleSpeed(); break;
            case 4: Application.Quit(); break;
        }
    }
}
