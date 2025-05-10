using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f; // adjust as needed
    private float destroyX = 0.64f;

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x >= destroyX)
        {
            Destroy(gameObject);
        }
    }
}
