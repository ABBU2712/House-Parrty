using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPos = target.position;
            newPos.z = -10f; // Keep the camera behind everything
            transform.position = newPos;
        }
    }
}
