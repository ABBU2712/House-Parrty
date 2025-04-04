using UnityEngine;

public class SpeakerHoverUI : MonoBehaviour
{
    public GameObject uiPanel; // Assign SpeakerMenu here
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        uiPanel.SetActive(false); // Hide by default
    }

    void Update()
    {
        // Only check when user clicks
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Speaker"))
                {
                    // Show menu in center of screen (Canvas space)
                    uiPanel.SetActive(true);
                }
                else
                {
                    uiPanel.SetActive(false);
                }
            }
            else
            {
                uiPanel.SetActive(false);
            }
        }
    }
}
