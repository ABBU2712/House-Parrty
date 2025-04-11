using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastController : MonoBehaviour
{
    public float rayLength = 10f;
    public LineRenderer lineRenderer;
    public LayerMask interactableLayer;
    public LayerMask FloorLayer;
    public Transform headTransform;

    private GameObject currentHighlighted;
    private GameObject heldObject;

    public void SetHeldObject(GameObject obj)
    {
        heldObject = obj;
    }

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.04f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void LateUpdate()
    {
        if (headTransform == null)
        {
            Debug.LogWarning("Head Transform not assigned in Inspector!");
            return;
        }

        Vector3 offset = headTransform.forward * 0.1f + headTransform.right * -0.05f + headTransform.up * -0.3f;
        Vector3 rayOrigin = headTransform.position + offset;
        Vector3 direction = (headTransform.forward + headTransform.up * 0.1f).normalized;

        Ray ray = new Ray(rayOrigin, direction);
        RaycastHit hit;

        lineRenderer.SetPosition(0, rayOrigin);
        lineRenderer.SetPosition(1, rayOrigin + direction * rayLength);
        Debug.DrawRay(rayOrigin, direction * rayLength, Color.green);


        if (Physics.Raycast(ray, out hit, rayLength))
        {
            lineRenderer.SetPosition(1, hit.point);
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag("Interactable"))
            {
                ObjectMenuHandler menu = hitObj.GetComponentInChildren<ObjectMenuHandler>();

                if (menu != null && Input.GetButtonDown("js7"))
                {
                    menu.ShowMenu(hitObj);
                }

                if (currentHighlighted != hitObj)
                {
                    ClearHighlight();
                    currentHighlighted = hitObj;
                    HighlightObject(currentHighlighted);
                }
            }
            else
            {
                ClearHighlight();
            }

            if (hitObj.CompareTag("Floor") && Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                Transform player = GameObject.FindWithTag("Player").transform;
                player.position = hit.point + Vector3.up * 1.0f;
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void HighlightObject(GameObject obj)
    {
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null)
        {
            outline = obj.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 5f;
        }
        outline.enabled = true;
    }

    void ClearHighlight()
    {
        if (currentHighlighted != null)
        {
            Outline outline = currentHighlighted.GetComponent<Outline>();
            if (outline != null)
                outline.enabled = false;

            currentHighlighted = null;
        }
    }
}
