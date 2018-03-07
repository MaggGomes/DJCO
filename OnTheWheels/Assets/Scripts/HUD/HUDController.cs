using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public GameObject car;
	public float fillAmountHealthBar;
	public Image contentHealthBar;
	public float fillAmountNitroBar;
	public Image contentNitroBar;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {			
		HandleBar ();
	}

	private void HandleBar(){
		Debug.Log (car.GetComponent<CarController> ().lifePoints);
		fillAmountHealthBar = car.GetComponent<CarController> ().lifePoints / 1000;
		fillAmountNitroBar = car.GetComponent<CarController> ().nitroTank;

		Debug.Log (car.GetComponent<CarController> ().nitroTank);

		if(fillAmountHealthBar != contentHealthBar.fillAmount)
			contentHealthBar.fillAmount = fillAmountHealthBar;

		if(fillAmountNitroBar != contentNitroBar.fillAmount)
			contentNitroBar.fillAmount = fillAmountNitroBar;
	}
}
