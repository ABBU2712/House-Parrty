using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController charCntrl;
    [Tooltip("The speed at which the character will move.")]
    public float speed = 5f;
    [Tooltip("The camera representing where the character is looking.")]
    public GameObject cameraObj;
    [Tooltip("Should be checked if using the Bluetooth Controller to move. If using keyboard, leave this unchecked.")]
    public bool joyStickMode;

    void Start()
    {
        charCntrl = GetComponent<CharacterController>();

        // Auto-assign the main camera if cameraObj is not set in the Inspector.
        if (cameraObj == null)
        {
            cameraObj = Camera.main?.gameObject;
            if (cameraObj == null)
            {
                Debug.LogError("Camera object not assigned and no Main Camera found. Please assign a camera object in the Inspector.");
            }
        }
    }

    void Update()
    {
        // Get horizontal and vertical movements.
        float horComp = Input.GetAxis("Horizontal");
        float vertComp = Input.GetAxis("Vertical");

        if (joyStickMode)
        {
            horComp = Input.GetAxis("Vertical");
            vertComp = Input.GetAxis("Horizontal") * -1;
        }

        Vector3 moveVect = Vector3.zero;

        // Get the look direction based on the camera.
        Vector3 cameraLook = cameraObj.transform.forward;
        cameraLook.y = 0f;
        cameraLook = cameraLook.normalized;

        Vector3 forwardVect = cameraLook;
        Vector3 rightVect = Vector3.Cross(forwardVect, Vector3.up).normalized * -1;

        moveVect += rightVect * horComp;
        moveVect += forwardVect * vertComp;
        moveVect *= speed;

        charCntrl.SimpleMove(moveVect);
    }
}
