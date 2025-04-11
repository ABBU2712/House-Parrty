using UnityEngine;

public class RaycastReticleManager : MonoBehaviour
{
    [Tooltip("Reference to the UI Reticle Pointer script.")]
    public ReticlePointer reticlePointer;

    [Tooltip("Infinite raycast distance.")]
    public float raycastDistance = Mathf.Infinity;

    private Camera cam;
    // Stores the current interactable component (if any) hit by the ray.
    private IRaycastInteractable currentInteractable;

    void Start()
    {
        cam = Camera.main;
        if (reticlePointer == null)
        {
            Debug.LogError("ReticlePointer is not assigned in RaycastReticleManager.");
        }
    }

    void Update()
    {
        // Cast a ray from the camera forward (infinite distance).
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(ray, out hit, raycastDistance);

        // Update the reticle color.
        if (reticlePointer != null)
        {
            reticlePointer.SetHighlighted(hitSomething);
        }

        // Get the interactable component from the hit object, if any.
        IRaycastInteractable hitInteractable = null;
        if (hitSomething)
        {
            hitInteractable = hit.collider.GetComponent<IRaycastInteractable>();
        }

        // If the hit interactable is different from the current one, update hover states.
        if (currentInteractable != hitInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.SetHovered(false);
            }
            currentInteractable = hitInteractable;
            if (currentInteractable != null)
            {
                currentInteractable.SetHovered(true);
            }
        }
    }
}
