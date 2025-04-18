using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] Transform createdObjectPrefab; 
    [SerializeField] Transform targetPos;

    public float maxDelay;

    private float currentDelay = 0;
       

    public void SpawnObject()
    {
        Vector3 rotation;
        rotation = new Vector3(0, 0, 0);

        BoxCollider2D zone = GetComponent<BoxCollider2D>();
        Transform createdObj = Instantiate(createdObjectPrefab, targetPos.position, Quaternion.Euler(rotation));
        createdObj.parent = transform;
    }

    private void Update()
    {
        if (currentDelay < maxDelay) currentDelay += Time.deltaTime;
        else
        {
            SpawnObject();
            currentDelay = 0;
        }
        
    }
          
}
