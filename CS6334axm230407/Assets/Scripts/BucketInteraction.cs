using UnityEngine;
using UnityEngine.UI;  // For UI elements
using System.Collections;

public class BucketInteraction : MonoBehaviour
{
    public GameObject disc;  // Reference to the disc inside the bucket
    public GameObject canvas;  // Reference to the canvas
    //public Text messageText;  // Reference to the message text on the canvas

    private void Start()
    {
        // Ensure the canvas is inactive initially
        canvas.SetActive(false);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.B)) {
            //StartCoroutine(ShowMessage());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ShowMessage());
        // Check if the object entering the trigger is the ball
       
        Debug.Log("triggered");
    }

    private IEnumerator ShowMessage()
    {
        // Enable the canvas and display the message
        canvas.SetActive(true);
        //messageText.text = "You scored!"; // Customize your message

        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Hide the canvas after 2 seconds
        canvas.SetActive(false);
    }
}
