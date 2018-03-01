using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float speedForce = 50f;
	float torqueForce = -25f;
	private Rigidbody2D rb2d;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Cars/cop");
	}

	void Update()
	{
		
	}

	void FixedUpdate ()
	{
			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
			if (Input.GetKey (KeyCode.UpArrow)) {
				rb2d.AddForce(transform.up * speedForce);
			}

			rb2d.AddTorque (Input.GetAxis ("Horizontal") * torqueForce);
	}
}
