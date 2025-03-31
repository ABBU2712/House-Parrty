using UnityEngine;

public class GrabObject : MonoBehaviour
{
   
    public GameObject myHand;
    public Transform controllerTransform;
    public float raycastRange = 10f;
    private bool handStatus = false;
    private Vector3 grabbedObjectPos;
    private GameObject grabbedObject;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
      
    }

    void Update()
    {
        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);
        RaycastHit hit;

        if (Input.GetButtonDown("js5"))
        {

            if (handStatus)
            {
                grabbedObject.transform.SetParent(null);
                handStatus = false;
                rb = grabbedObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (!handStatus)
                {
                    grabbedObject = hitObject;
                    rb = grabbedObject.GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    grabbedObject.transform.SetParent(myHand.transform);
                    grabbedObject.transform.localPosition = myHand.transform.localPosition;
                    handStatus = true;
                }
            }


        }


    }

}
