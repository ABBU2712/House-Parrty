using UnityEngine;

public class RotateInteraction : MonoBehaviour, IRaycastInteractable
{
    [Tooltip("Rotation speed (degrees per second).")]
    public float rotationSpeed = 90f;

    [Tooltip("Axis of rotation (e.g. (0,1,0) for Y-axis).")]
    public Vector3 rotationAxis = Vector3.up;

    private bool isHovered = false;

    public void SetHovered(bool hovered)
    {
        isHovered = hovered;
    }

    void Update()
    {
        // When hovered and the "X" button is held, rotate the object.
        if (isHovered && Input.GetButton("js2"))
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
