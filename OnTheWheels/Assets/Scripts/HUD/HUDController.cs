using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
	public GameObject Player;
	public GameObject Opponent;
	public List<GameObject> CheatSheets;
	public GameObject PlayerArrow;
	public List<GameObject> CheatSheetArrows;
	public GameObject EndArrow;
	public CameraController Camera;
	public Vector2 ArrowOrigin;
	public int pickedUp = 0;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		PlayerArrow = new GameObject ();
		Image PlayerArrowImage = PlayerArrow.AddComponent<Image> ();
		PlayerArrowImage.sprite = Resources.Load<Sprite> ("HUD/playerarrow");
		PlayerArrow.GetComponent<RectTransform> ().SetParent (gameObject.transform);
		PlayerArrow.SetActive (true);
		RectTransform rt = PlayerArrow.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2(35, 50);
		if (gameObject.tag == "HUD1") {
			ArrowOrigin = new Vector2(-200, 0);		}
		else if (gameObject.tag == "HUD2") {
			ArrowOrigin = new Vector2(200, 0);
		}
		PlayerArrow.transform.localScale = new Vector2(0.2f, 0.2f);
		rt.localPosition = ArrowOrigin;

		if (CheatSheets != null) {
			CheatSheetArrows = new List<GameObject> (CheatSheets.Count);
			for (int i = 0; i < CheatSheets.Count; i++) {
				GameObject CSArrow = new GameObject ();
				Image CSArrowImage = CSArrow.AddComponent<Image> ();
				CSArrowImage.sprite = Resources.Load<Sprite> ("HUD/cheatsheetarrow");
				CSArrow.GetComponent<RectTransform> ().SetParent (gameObject.transform);
				CSArrow.SetActive (true);
				rt = CSArrow.GetComponent<RectTransform> ();
				rt.sizeDelta = new Vector2(35, 50);
				CSArrow.transform.localScale = new Vector2(0.2f, 0.2f);
				rt.localPosition = ArrowOrigin;
				CheatSheetArrows.Add(CSArrow);
			}
		}

		if (gameObject.tag == "HUD1") {
			EndArrow = new GameObject ();
			Image EndArrowImage = EndArrow.AddComponent<Image> ();
			EndArrowImage.sprite = Resources.Load<Sprite> ("HUD/endarrow");
			EndArrow.GetComponent<RectTransform> ().SetParent (gameObject.transform);
			EndArrow.SetActive (false);
			rt = EndArrow.GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2(35, 50);
			EndArrow.transform.localScale = new Vector2(0.2f, 0.2f);
			rt.localPosition = ArrowOrigin;
		}
	}

	void Update(){
		float t = Time.time - startTime;
		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f2");
		timer.text = minutes + ":" + seconds;

		Vector3 direction = Opponent.transform.position - Player.transform.position;
		PlayerArrow.transform.rotation = Quaternion.FromToRotation(Vector3.right, direction) * Quaternion.Inverse(Camera.transform.rotation);
		Vector2 targetForward = PlayerArrow.transform.rotation * Vector3.right;
		PlayerArrow.GetComponent<RectTransform> ().localPosition = ArrowOrigin + targetForward * 30;

		for (int i = 0; i < CheatSheets.Count; i++) {
			if (CheatSheets[i] != null) {
				direction = CheatSheets[i].transform.position - Player.transform.position;
				CheatSheetArrows[i].transform.rotation = Quaternion.FromToRotation(Vector3.right, direction) * Quaternion.Inverse(Camera.transform.rotation);
				targetForward = CheatSheetArrows[i].transform.rotation * Vector3.right;
				CheatSheetArrows[i].GetComponent<RectTransform> ().localPosition = ArrowOrigin + targetForward * 30;
			} else if (CheatSheetArrows[i] != null) {
				Destroy(CheatSheetArrows[i]);
				CheatSheetArrows[i] = null;
				pickedUp++;
			}
		}

		if (pickedUp == CheatSheets.Count && CheatSheets.Count != 0) {
			if (!EndArrow.activeInHierarchy) {
				EndArrow.SetActive (true);
			}
			direction = new Vector3(2872, -3390, 0) - Player.transform.position;
			EndArrow.transform.rotation = Quaternion.FromToRotation(Vector3.right, direction) * Quaternion.Inverse(Camera.transform.rotation);
			targetForward = EndArrow.transform.rotation * Vector3.right;
			EndArrow.GetComponent<RectTransform> ().localPosition = ArrowOrigin + targetForward * 30;
		}
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

	public void Restart(){
		SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
	}
}
