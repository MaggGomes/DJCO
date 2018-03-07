using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

	public float fillAmount;
	public Image content;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {		
		HandleBar ();
	}

	private void HandleBar(){
		Debug.Log (content.fillAmount);
		if(fillAmount != content.fillAmount)
			content.fillAmount = fillAmount;
	}
}
