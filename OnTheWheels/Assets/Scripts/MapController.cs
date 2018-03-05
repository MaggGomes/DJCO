using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {


    Vector3[] startingPositions = {
        new Vector3(3857, -3716, 0)
    };
    GameObject Player;
    Vector3[] copStartingPositions = {
        new Vector3(3857, -3760, 0)
    };
    GameObject Cop;


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
        //TODO initialize player with choosen car attributes
        Player = GameObject.Instantiate(Resources.Load("Car") as GameObject);
        Player.transform.position = startingPositions[0];
        Player.tag = "Player";


        //TODO initialize cop with AI or controlled
        Cop = GameObject.Instantiate(Resources.Load("Car") as GameObject);
        Cop.transform.position = copStartingPositions[0];
        Cop.GetComponent<CarController>().playerControlled = true;
        Cop.GetComponent<CarController>().throttleKey = KeyCode.W;
        Cop.GetComponent<CarController>().brakeKey = KeyCode.S;
        Cop.GetComponent<CarController>().leftKey = KeyCode.A;
        Cop.GetComponent<CarController>().rightKey = KeyCode.D;
        Cop.GetComponent<CarController>().handbrakeKey = KeyCode.Q;
        Cop.GetComponent<CarController>().nitroKey = KeyCode.E;
        Cop.GetComponent<CarController>().sprite = Resources.Load<Sprite>("Cars/cop");
        Cop.GetComponent<CarController>().isCop = true;
        Cop.tag = "Cop";

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
