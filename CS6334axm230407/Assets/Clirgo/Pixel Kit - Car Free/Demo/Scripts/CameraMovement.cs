using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float minX;
    [SerializeField] float maxX;    

    void Update()
    {
        float displacement = 0;

        if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (transform.position.x < maxX))
        {
            
            displacement = 1;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (transform.position.x > minX))
        {
            //if(transform.position.x > 0)
                displacement = -1;
        }



        transform.position = new Vector3(transform.position.x + (displacement * speed * Time.deltaTime), 
                                         transform.position.y, 
                                         transform.position.z);
    }
}
