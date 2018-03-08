using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public Transform target;

	[Range(0.1f, 5.0f)]
	public float rotationSmoothness = 1f;

	void Start ()
	{
		
    }

	void LateUpdate ()
	{
        // Do your stuff here
		transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.fixedDeltaTime * 1f);
        transform.position = new Vector3 (target.position.x, target.position.y, -5f);
	}
}
