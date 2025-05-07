using UnityEngine;

public class CircularPathMover : MonoBehaviour
{
    public Transform centerPoint; // Empty GameObject at center of the circle
    public float radius = 5f;
    public float angularSpeed = 30f; // degrees per second
    private float angle;

    void Update()
    {
        angle += angularSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
        transform.position = centerPoint.position + offset;

        // Optional: Rotate car to face movement direction
        Vector3 direction = offset.normalized;
        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
