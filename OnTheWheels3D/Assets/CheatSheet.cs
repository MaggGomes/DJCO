using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSheet : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		print ("collision cheatsheet");
		if (other.tag == "PlayerTag") {
			// TODO - fazer update do score de cábulas
			//other.GetComponent<CarController>.updateScore ();

			Destroy (this.gameObject);
		}	

		Destroy (this.gameObject);
	}
}
