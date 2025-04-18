using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    internal void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
