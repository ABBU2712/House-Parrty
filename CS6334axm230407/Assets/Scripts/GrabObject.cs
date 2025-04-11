using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private GameObject grabbedObject;
    public GameObject myHand;
    public Transform controllerTransform;
    private float raycastRange = 3f;
    private bool inHands = false;
    private Vector3 grabbedObjectPos;
    Rigidbody rb;
    public LineRenderer lineRenderer;
    public Ray ray;
    private Vector3 offset;
    private Vector3 rayOrigin;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.02f;

        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = Color.red;

        offset = transform.forward * 0.1f + transform.right * -0.05f + transform.up * -0.3f; // forward and slightly left
        rayOrigin = transform.position + offset;
        direction = (transform.forward + transform.up * 0.3f).normalized;

        ray = new Ray(rayOrigin, direction);
    }

    void Update()
    {
        //Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
        offset = transform.forward * 0.1f + transform.right * -0.05f + transform.up * -0.3f; // forward and slightly left
        rayOrigin = transform.position + offset;
        direction = (transform.forward + transform.up * 0.3f).normalized;
        ray = new Ray(rayOrigin, direction);
        RaycastHit hit;
        Vector3 endPoint = rayOrigin + direction * raycastRange;

        if(Input.GetKeyDown(KeyCode.B)) {

            if(inHands) {
                grabbedObject.transform.SetParent(null);
                grabbedObject.transform.rotation = Quaternion.identity;
                inHands = false;
                rb = grabbedObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                Debug.Log("Currently not in hands: " + inHands);
                Debug.Log("Object released!");
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
                GameObject hitObject = hit.collider.gameObject;
                         Debug.Log("this is hit object" + hitObject);
                        Debug.Log("Button Pressed");
                        Debug.Log("Currently in hands: " + inHands);
                        if(!inHands && hitObject.CompareTag("Grabable")) {
                            grabbedObject = hitObject;
                            Debug.Log("this is grabbed" + grabbedObject);
                            rb = grabbedObject.GetComponent<Rigidbody>();
                            Debug.Log("this is rb" + rb);
                            rb.isKinematic = true;
                            rb.useGravity = false;
                            grabbedObject.transform.SetParent(myHand.transform);
                            grabbedObject.transform.localPosition = Vector3.zero;
                            grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, 2.0f, grabbedObject.transform.position.z);
                            inHands = true;
                            Debug.Log("Object grabbed!");
                        }
            }

        }


        lineRenderer.SetPosition(0, rayOrigin);
        lineRenderer.SetPosition(1, endPoint);

        Debug.DrawRay(transform.position, transform.forward * raycastRange, Color.green);


    }

}
