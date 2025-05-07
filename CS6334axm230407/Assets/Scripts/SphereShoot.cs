using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SphereShoot : MonoBehaviour
{
    public Camera cam;
    private LineRenderer lineRenderer;
    public Image powerBar; // UI fill image
    public float maxPower = 500f;
    public float fillSpeed = 0.5f;

    public GameObject pinParent; // Parent containing all pins
    public TextMeshProUGUI scoreText; // Reference to UI text
    public float pinCheckDelay = 5f; // Delay before checking pins
    public TextMeshProUGUI playButtonText;

    private float currentPower = 0f;
    private bool isCharging = false;
    private Rigidbody rb;
    private int score = 0;
    private bool gameOn = false;

    Vector3 startPos;
    Vector3 direction;
    Vector3 endPos;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        lineRenderer.enabled = false;
        powerBar.fillAmount = 0f;
        scoreText.text = "Points: 0";
        gameOn = false;
    }

    void Update()
    {
        Debug.Log(gameOn);
        direction = cam.transform.forward;
        startPos = transform.position;
        endPos = startPos + direction * 5f;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

        if ((Input.GetKey(KeyCode.P) || Input.GetButton("js3")) && gameOn)
        {
            if (!isCharging)
            {
                isCharging = true;
                lineRenderer.enabled = true;
            }

            currentPower += fillSpeed * Time.deltaTime;
            currentPower = Mathf.Clamp01(currentPower);
            powerBar.fillAmount = currentPower;
        }

        if ((Input.GetKeyUp(KeyCode.P) || Input.GetButtonUp("js3")) && gameOn)
        {
            Vector3 forceDir = (endPos - startPos).normalized;
            rb.AddForce(forceDir * (currentPower * maxPower));
            currentPower = 0f;
            powerBar.fillAmount = 0f;
            isCharging = false;
            gameOn = false;
            lineRenderer.enabled = false;

            StartCoroutine(CheckPinsAfterDelay());
        }
    }

    IEnumerator CheckPinsAfterDelay()
    {
        yield return new WaitForSeconds(pinCheckDelay);

        int fallenPins = 0;
        foreach (Transform pin in pinParent.transform)
        {
            Vector3 rotation = pin.eulerAngles;
            float xRot = Mathf.Abs(NormalizeAngle(rotation.x));
            float zRot = Mathf.Abs(NormalizeAngle(rotation.z));

            if (xRot > 5f || zRot > 5f)  // Assuming more than 5 degrees means fallen
            {
                fallenPins++;
            }
        }

        score += fallenPins * 5;
        scoreText.text = "Points: " + score;
        
    }

    float NormalizeAngle(float angle)
    {
        return angle > 180 ? angle - 360 : angle;
    }

    public void GameStartStop() {
        gameOn = !gameOn;

        if(!gameOn) {
            score = 0;
            scoreText.text = "Points: " + score;
            playButtonText.text = "Play";
            lineRenderer.enabled = false;
        }
        else{
            playButtonText.text = "End";
        }
    }

    public void ResetPins()
    {
        // Transform pin = pinParent.transform.GetChild(0);
        // pin.localPosition = new Vector3(0, 1, 0); // relative to parent
        //     pin.localRotation = Quaternion.Euler(0, 0, 0);

        Vector3[] positions = new Vector3[]
        {
            // Row 1 (4 pins)
            new Vector3(-1.0f, 0.5f, 4.5f),
            new Vector3(-0.5f, 0.5f, 4.5f),
            new Vector3(0f, 0.5f, 4.5f),
            new Vector3(0.5f, 0.5f, 4.5f),

            // Row 2 (3 pins)
            new Vector3(-0.75f, 0.5f, 4f),
            new Vector3(-0.25f, 0.5f, 4f),
            new Vector3(0.25f, 0.5f, 4f),

            // Row 3 (2 pins)
            new Vector3(-0.5f, 0.5f, 3.5f),
            new Vector3(0f, 0.5f, 3.5f),

            // Row 4 (1 pin)
            new Vector3(-0.25f, 0.5f, 3f),
        };

        for (int i = 0; i < pinParent.transform.childCount && i < positions.Length; i++)
        {
            Transform pin = pinParent.transform.GetChild(i);
            pin.localPosition = positions[i];
            pin.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(-0.18f, 1f, 0f);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameOn = true;
        
    }


}
