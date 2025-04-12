using UnityEngine;

public class Bowl : MonoBehaviour
{
    public float moveForce = 10f;  // The force applied to move the sphere
    public GameObject ball;           // Reference to the Rigidbody component
    private Rigidbody rb;
    public GameObject cam;

    void Start()
    {
        // Get the Rigidbody component if it's not already assigned
        // if (ball != null)
        // {
            rb = ball.GetComponent<Rigidbody>();
        // }
    }

    void Update()
    {
        // Apply force to the sphere's Rigidbody in the forward direction
        // Only apply force when pressing a specific key (e.g., "W" or arrow keys)
        if (Input.GetKeyDown(KeyCode.Z) && ball!=null)
        {
            // Apply force in the forward direction (relative to the object’s orientation)

            rb.AddForce(cam.transform.forward * moveForce);
        }
    }
}
