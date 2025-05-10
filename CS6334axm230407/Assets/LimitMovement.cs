using UnityEngine;

public class LimitYMovement : MonoBehaviour
{

    public float minY = 0.18f;
    public float maxY = 0.74f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = (float)Mathf.Round(Mathf.Clamp(pos.y, minY, maxY) * 1000f) / 1000f;
        transform.position = pos;
    }
}
