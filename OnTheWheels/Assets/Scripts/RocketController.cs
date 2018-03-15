using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour {
	public CarController Car;
	private bool destroyed = false;
	private Vector3 velocity;
	private float rocketSpeed = 10f;
	private float destroyTime = 0.5f;
	private Sprite destroyedSprite;
	private CarController CarHit = null;

	void Start () {
		velocity = new Vector3 (0, 0, 0);
		destroyedSprite = Resources.Load<Sprite> ("Cars/Explosion");
	}

	void Update () {
		if (Car != null) {
			transform.rotation = Car.transform.rotation;
			transform.position = Car.transform.position;
		} else if (!destroyed) {
			transform.position += velocity.normalized * rocketSpeed;
		} else if (CarHit != null) {
			transform.rotation = CarHit.transform.rotation;
			transform.position = CarHit.transform.position;
		}

		if (destroyed) {
			if (destroyTime > 0) {
				destroyTime -= Time.deltaTime;
			} else {
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		destroyed = true;
		gameObject.GetComponent<SpriteRenderer>().sprite = destroyedSprite;
		if (other.gameObject.tag == "Cop" || other.gameObject.tag == "Player") {
			CarHit = other.gameObject.GetComponent<CarController> ();
			CarHit.lifePoints -= 500;
		}
	}

	public void Launch() {
		velocity = Car.rb2d.transform.up;
		Car = null;
	}
}