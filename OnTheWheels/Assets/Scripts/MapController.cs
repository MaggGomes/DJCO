using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	public static bool CopHumanController = true;

    Vector3[] startingPositions = {
        new Vector3(3857, -3716, 0)
    };
    public GameObject Player;
    Vector3[] copStartingPositions = {
        new Vector3(3857, -3760, 0)
    };
    public GameObject Cop;

    Vector3[] powerUpsPositions = {
        new Vector3(3900, -3115, 0),
        new Vector3(3945, -2625, 0),
    };
    GameObject PowerUp;

    Vector3[] cheatsheetsPositions = {
        new Vector3(3930, -3115, 0),
        new Vector3(3970, -2625, 0),
        new Vector3(3970, -2660, 0),
    };
    GameObject Cheatsheet;

    // Use this for initialization
    void Start ()
    {
        //TODO initialize cop with AI or controlled
        Cop.transform.position = copStartingPositions[0];
		Cop.GetComponent<CarController>().playerControlled = MapController.CopHumanController;
        Cop.GetComponent<CarController>().throttleKey = KeyCode.W;
        Cop.GetComponent<CarController>().brakeKey = KeyCode.S;
        Cop.GetComponent<CarController>().leftKey = KeyCode.A;
        Cop.GetComponent<CarController>().rightKey = KeyCode.D;
        Cop.GetComponent<CarController>().handbrakeKey = KeyCode.Q;
        Cop.GetComponent<CarController>().nitroKey = KeyCode.E;
        Cop.tag = "Cop";
        Cop.name = "Cop";

        //TODO initialize player with choosen car attributes
        Player.transform.position = startingPositions[0];
        Player.tag = "Player";
        Player.name = "Player";

        // assign camera to player car
        GameObject.FindGameObjectWithTag("Camera1").GetComponent<CameraController>().target = Player.transform;

        // assign main camera to player cop
		if (Cop.GetComponent<CarController> ().playerControlled) {
			GameObject.FindGameObjectWithTag ("Camera1").GetComponent<Camera> ().rect = new Rect (0f, 0f, 0.5f, 1f);
			GameObject.FindGameObjectWithTag ("Camera2").GetComponent<CameraController> ().target = Cop.transform;
			GameObject.FindGameObjectWithTag ("Camera2").GetComponent<Camera> ().rect = new Rect (0.5f, 0f, 0.5f, 1f);
		} else {
			// Disables Camera 2
			GameObject.FindGameObjectWithTag ("Camera2").GetComponent<CameraController> ().gameObject.SetActive (false);
		}

        foreach (Vector3 powerUpPosition in powerUpsPositions) {
            // These should be pooled and re-used
            PowerUp = GameObject.Instantiate(Resources.Load("PowerUp") as GameObject);

            PowerUp.transform.position = powerUpPosition;
        }

        foreach (Vector3 cheatsheetPosition in cheatsheetsPositions)
        {
            // These should be pooled and re-used
            Cheatsheet = GameObject.Instantiate(Resources.Load("Cheatsheet") as GameObject);

            Cheatsheet.transform.position = cheatsheetPosition;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
