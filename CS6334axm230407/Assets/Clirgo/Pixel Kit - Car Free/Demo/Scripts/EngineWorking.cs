using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineWorking : MonoBehaviour
{
    float speed = 6f;
    [SerializeField] float timer = 0f;

    private void Start()
    {
        timer = Random.Range(0, 100);
    }

    void Update()
    {
        timer += Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y + Mathf.Sin(timer)/1000 * 0.5f, 
                                         transform.position.z);
    }
}
