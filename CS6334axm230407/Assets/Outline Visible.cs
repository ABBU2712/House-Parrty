using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Public variables to adjust outline color and width.
    public Color outlineColor = Color.yellow;
    public float outlineWidth = 5f;

    // Optional reference to the reticle pointer (if you want the reticle to update too).
    public ReticlePointer reticlePointer;

    // Reference to the Outline component.
    private Outline outline;

    void Awake()
    {
        // Check for an existing Outline component; if not present, add one.
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        // Configure the outline settings.
        outline.OutlineMode = Outline.Mode.OutlineVisible; // Ensure the correct mode is set.
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;

        // Disable the outline initially.
        outline.enabled = false;
    }

    // Enable outline and update the reticle when the pointer enters.
    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
        if (reticlePointer != null)
        {
            reticlePointer.SetHighlighted(true);
        }
    }

    // Disable outline and reset the reticle when the pointer exits.
    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
        if (reticlePointer != null)
        {
            reticlePointer.SetHighlighted(false);
        }
    }
}
