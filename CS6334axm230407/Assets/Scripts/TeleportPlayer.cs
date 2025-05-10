using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    //public Transform controllerTransform; // Assign the VR Controller or Camera
    public float raycastRange = 20f; // Max teleport distance
    private Ray ray;
    public GameObject cam;
    private CharacterController controller;
    public Transform reticlePointer;
    public float rayDistance = 100f;
    //public GameObject character;

    void Start() {
        controller = GetComponent<CharacterController>();
        //grabObject = cam.GetComponent<GrabObject>();
    }
    void Update()
    {
        if (Input.GetButton("js3") || Input.GetKeyDown(KeyCode.A)) // ✅ Teleport on button press
        {
            Debug.Log("Tello ");
            TryTeleport();
        }
    }

    void TryTeleport()
    {
        //ray = grabObject.getRay();
        //RaycastHit hit;

        Ray ray = new Ray(reticlePointer.position, reticlePointer.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Floor"))
            {
                controller.enabled = false;
                transform.position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z); // ✅ Teleport to the hit location
                Debug.Log("Teleported to: " + hit.point);
                controller.enabled = true;
            }
            else
            {
                Debug.Log("Not a valid teleport location.");
            }
        }
    }
}
