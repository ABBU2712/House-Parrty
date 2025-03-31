using UnityEngine;

public class TranslateInteraction : MonoBehaviour, IRaycastInteractable
{
    [Tooltip("Speed of translation (units per second).")]
    public float moveSpeed = 2f;

    [Tooltip("Direction of movement (e.g. (1,0,0) to move right).")]
    public Vector3 moveDirection = Vector3.forward;

    private bool isHovered = false;

    public void SetHovered(bool hovered)
    {
        isHovered = hovered;
    }

    void Update()
    {
        // When hovered and the "X" button is held, translate the object.
        if (isHovered && Input.GetButton("js2"))
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
