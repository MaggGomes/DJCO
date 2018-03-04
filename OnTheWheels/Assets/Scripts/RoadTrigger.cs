using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTrigger : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
	{
		CarController car = other.gameObject.GetComponent<CarController> ();
		car.terrain["road"] = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		CarController car = other.gameObject.GetComponent<CarController> ();
		car.terrain["road"] = false;
	}
}
