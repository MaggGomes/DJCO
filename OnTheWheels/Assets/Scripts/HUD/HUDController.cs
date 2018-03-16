using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour {

	public GameObject car;
	public float fillAmountHealthBar;
	public Image contentHealthBar;
	public float fillAmountNitroBar;
	public Image contentNitroBar;
	public int cheatSheetCounter = 0;
	public TextMeshProUGUI cheatSheetText;
	public Text timer;
	private float startTime = 0;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	void Update(){
		float t = Time.time - startTime;
		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f2");
		timer.text = minutes + ":" + seconds;
	}
		

	// Update is called once per frame
	void LateUpdate () {			
		HandleBar ();
	}

	private void HandleBar(){
		//Debug.Log (car.GetComponent<CarController> ().lifePoints);
		fillAmountHealthBar = car.GetComponent<CarController> ().lifePoints / 1000;
		fillAmountNitroBar = car.GetComponent<CarController> ().nitroTank;
		cheatSheetCounter = car.GetComponent<CarController> ().cheatsheetsCaught;

		//Debug.Log (car.GetComponent<CarController> ().nitroTank);

		if(fillAmountHealthBar != contentHealthBar.fillAmount)
			contentHealthBar.fillAmount = fillAmountHealthBar;

		if(fillAmountNitroBar != contentNitroBar.fillAmount)
			contentNitroBar.fillAmount = fillAmountNitroBar;

		if (!car.GetComponent<CarController>().isCop && cheatSheetCounter.ToString() != cheatSheetText.text)
			cheatSheetText.text = cheatSheetCounter.ToString();
	}
}
