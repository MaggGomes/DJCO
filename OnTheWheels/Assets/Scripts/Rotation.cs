using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	private float angle = 0f;

	void Start () {
		
	}

	void Update () {
		angle += 1f % 360f;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
