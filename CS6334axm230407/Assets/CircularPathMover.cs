using UnityEngine;

public class CircularPathMover : MonoBehaviour
{
    public Transform centerPoint; // Center of the circle
    public float radius = 5f;
    public float angularSpeed = 30f; // degrees per second
    private float angle;

    void Update()
    {
        angle += angularSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        // Calculate 2D circular offset
        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;

        // Lock Z = 0 on center and position
        Vector3 center2D = new Vector3(centerPoint.position.x, centerPoint.position.y, 0);
        transform.position = center2D + offset;

        // Optional: Rotate to face movement direction (still Z-locked)
        Vector3 direction = offset.normalized;
        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
