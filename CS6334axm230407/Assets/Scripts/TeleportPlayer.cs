using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    //public Transform controllerTransform; // Assign the VR Controller or Camera
    public float raycastRange = 20f; // Max teleport distance
    private Ray ray;
    public GameObject cam;
    private CharacterController controller;
    public GameObject character;
    private GrabObject grabObject;
    
    void Start() {
        controller = GetComponent<CharacterController>();
        grabObject = cam.GetComponent<GrabObject>();
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
        ray = grabObject.getRay();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Floor")) // ✅ Check if hit object has "Floor" tag
            {
                controller.enabled = false;
                character.transform.position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z); // ✅ Teleport to the hit location
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
