using UnityEngine;

public class DJRayInteraction : MonoBehaviour
{
    public GrabObject raycastSource;

    private DJMenuControllerFixed currentDJMenu;
    private GameObject currentMenuObject;
    private bool isHoveringDJ = false;

    private void Update()
    {
        Ray ray = raycastSource.ray;
        RaycastHit hit;

        bool hitDJBox = false;

        if (Physics.Raycast(ray, out hit, 10f))
        {
            DJBoxController dj = hit.collider.GetComponentInParent<DJBoxController>();

            if (dj != null)
            {
                hitDJBox = true;

                if (!isHoveringDJ)
                {
                    // First time entering the DJ Box
                    Transform menuTransform = dj.transform.Find("DJMenu");
                    if (menuTransform != null)
                    {
                        currentMenuObject = menuTransform.gameObject;
                        currentMenuObject.SetActive(true);
                        currentMenuObject.transform.LookAt(Camera.main.transform);
                        currentMenuObject.transform.Rotate(0, 180f, 0);
                        currentDJMenu = currentMenuObject.GetComponent<DJMenuControllerFixed>();
                    }
                    isHoveringDJ = true;
                }
            }
        }

        // If ray is NOT hitting DJ Box anymore
        if (!hitDJBox && isHoveringDJ)
        {
            if (currentMenuObject != null)
                currentMenuObject.SetActive(false); // hide menu but DON'T stop audio

            isHoveringDJ = false;
            currentDJMenu = null;
        }

        // Song switching logic
        if (currentDJMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetButton("js2"))
                currentDJMenu.PlayRock();

            if (Input.GetKeyDown(KeyCode.Y) || Input.GetButton("js3"))
                currentDJMenu.PlayClassical();

            if (Input.GetKeyDown(KeyCode.A) || Input.GetButton("js0"))
                currentDJMenu.PlayEDM();

            //if (Input.GetKeyDown(KeyCode.B) || Input.GetButton("js1"))
            //    currentDJMenu.PlayJazz();
        }
    }
}
