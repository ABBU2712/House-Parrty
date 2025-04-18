using UnityEngine;
using System.Collections;

public class WheelRotate : MonoBehaviour {
	
	public int speed = 100;	
	
	void Update ()
    {
		transform.RotateAround(transform.position, new Vector3(0,0,1), Time.deltaTime*speed);		
	}
}
