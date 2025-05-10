using UnityEngine;

public class TVRayInteraction : MonoBehaviour
{
    //public GrabObject raycastSource;
    public Transform reticlePointer;
    public float rayDistance = 100f;
    public GameObject racingGameRoot;
    public BulletSpawner bulletSpawner; // Drag it in Inspector
    public MonoBehaviour movementScript;
    public string speedFieldName = "moveSpeed";

    private float originalSpeed;
    private bool isHoveringTV = false;
    private bool racingStarted = false;

    void Start()
    {
        var type = movementScript.GetType();
        var field = type.GetField(speedFieldName);
        if (field != null)
            originalSpeed = (float)field.GetValue(movementScript);
    }

    void Update()
    {
        Ray ray = new Ray(reticlePointer.position, reticlePointer.forward);
        RaycastHit hit;
        bool hitTV = false;

        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.collider.CompareTag("TV"))
            {
                hitTV = true;

                if (!isHoveringTV)
                    isHoveringTV = true;

                if ((Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("js2")) && !racingStarted)
                {
                    racingGameRoot.SetActive(true);
                    SetPlayerSpeed(0f);
                    racingStarted = true;
                    bulletSpawner.StartFiring(); // ✅ Now bullets start only after TV interaction
                }
            }
        }
        if ((Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("js2")) && racingStarted)
        {
            racingGameRoot.SetActive(false);
            RestorePlayerControl();
        }

        if (!hitTV && isHoveringTV)
            isHoveringTV = false;
    }

    private void SetPlayerSpeed(float value)
    {
        var type = movementScript.GetType();
        var field = type.GetField(speedFieldName);
        if (field != null)
            field.SetValue(movementScript, value);
    }

    public void RestorePlayerControl()
    {
        SetPlayerSpeed(originalSpeed);
        racingStarted = false;
    }
}
