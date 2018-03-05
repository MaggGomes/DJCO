using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public Transform target;

	void Start ()
	{
    }

	void LateUpdate ()
	{
        // Do your stuff here
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.time * 0.2f);
        transform.position = new Vector3 (target.position.x, target.position.y, -5f);
	}
}
