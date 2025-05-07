using UnityEngine;
using UnityEngine.SceneManagement;

public class TVInteraction : MonoBehaviour
{
    public float rayDistance = 10f;
    public KeyCode interactionKey = KeyCode.E ; // Or use controller button

    void Update()
    {
        if (Input.GetKeyDown(interactionKey) || Input.GetButton("js2"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.CompareTag("TV"))
                {
                    Debug.Log("TV Hit! Loading racing scene...");
                    SceneManager.LoadScene("Racinggame"); // change name if needed
                }
            }
        }
    }
}
