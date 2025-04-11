using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform trans; // Assign the VR Controller or Camera
    public float raycastRange = 20f; // Max teleport distance
    public GameObject character;
    private CharacterController controller;
    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.GetButtonDown("js3")) 
        {
            Debug.Log("Teleportation successful ");
            TryTeleport();
        }
    }

    void TryTeleport()
    {
        Ray ray = new Ray(trans.position, trans.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Floor"))
            {
                controller.enabled = false;
                character.transform.position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z); 
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

