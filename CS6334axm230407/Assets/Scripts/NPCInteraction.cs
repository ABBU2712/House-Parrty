using UnityEngine;

public class NPCRayInteraction : MonoBehaviour
{
    public GrabObject raycastSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Ray ray = raycastSource.ray;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f))
            {
                Animator npcAnim = hit.collider.GetComponentInParent<Animator>();
                SimpleNPC npcScript = hit.collider.GetComponentInParent<SimpleNPC>();

                if (npcAnim != null)
                {
                    npcAnim.SetBool("isDancing", false);
                    npcAnim.SetBool("isTalking", false);
                }

                if (npcScript != null)
                {
                    ShowMenu(npcScript);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Ray ray = raycastSource.ray;
            RaycastHit hit;
            Debug.Log("X is hit");

            if (Physics.Raycast(ray, out hit, 10f))
            {
                Animator npcAnim = hit.collider.GetComponentInParent<Animator>();
                SimpleNPC npcScript = hit.collider.GetComponentInParent<SimpleNPC>();

                if (npcAnim != null)
                    npcAnim.SetBool("isTalking", true);

                if (npcScript != null)
                {
                    npcScript.RespondTo("hello");
                    HideMenu(npcScript);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Ray ray = raycastSource.ray;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f))
            {
                Animator npcAnim = hit.collider.GetComponentInParent<Animator>();
                SimpleNPC npcScript = hit.collider.GetComponentInParent<SimpleNPC>();

                if (npcAnim != null)
                {
                    npcAnim.SetBool("isDancing", true);
                    Debug.Log("isDancing = true");
                }

                if (npcScript != null)
                {
                    npcScript.RespondTo("dance");
                    HideMenu(npcScript);
                }
            }
        }


        void ShowMenu(SimpleNPC npc)
        {
            Transform menuTransform = npc.transform.Find("InteractionMenu");

            if (menuTransform == null)
                return;

            GameObject menu = menuTransform.gameObject;
            menu.SetActive(true);
            menu.transform.LookAt(Camera.main.transform);
            menu.transform.Rotate(0, 180f, 0);

            var menuScript = menu.GetComponent<NPCActionMenu>();
            if (menuScript != null)
                menuScript.Setup(npc);
        }
        void HideMenu(SimpleNPC npc)
        {
            Transform menuTransform = npc.transform.Find("InteractionMenu");

            if (menuTransform == null)
                return;

            GameObject menu = menuTransform.gameObject;
            menu.SetActive(false);
        }
    }
}
