using UnityEngine;

public class Car2DController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = -Input.GetAxis("Horizontal"); // Negative to rotate correctly
    }

    void FixedUpdate()
    {
        // Move forward/backward
        rb.linearVelocity = transform.up * moveInput * moveSpeed;

        // Rotate left/right
        rb.MoveRotation(rb.rotation + turnInput * turnSpeed * Time.fixedDeltaTime);
    }
}
