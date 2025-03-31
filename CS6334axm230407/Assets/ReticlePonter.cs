using UnityEngine;
using UnityEngine.UI;

public class ReticlePointer : MonoBehaviour
{
    [Tooltip("The UI Image used for the reticle.")]
    public Image reticleImage;

    [Tooltip("Reticle color when nothing is hit.")]
    public Color normalColor = Color.white;

    [Tooltip("Reticle color when an interactable object is hit.")]
    public Color highlightedColor = Color.red;

    public void SetHighlighted(bool highlighted)
    {
        if (reticleImage != null)
        {
            reticleImage.color = highlighted ? highlightedColor : normalColor;
        }
    }
}
