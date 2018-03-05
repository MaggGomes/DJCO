using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public Transform target;

	void Start ()
	{

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

	void LateUpdate ()
	{
        // Do your stuff here
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.fixedDeltaTime * 5f);
        transform.position = new Vector3 (target.position.x, target.position.y, -5f);
	}
}
