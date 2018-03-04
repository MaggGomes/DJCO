using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {



    Vector3[] powerUpsPositions = {
        new Vector3(3900, -3115, 0),
        new Vector3(3945, -2625, 0),
    };

    Vector3[] cheatsheetsPositions = {
        new Vector3(3930, -3115, 0),
        new Vector3(3970, -2625, 0),
        new Vector3(3970, -2660, 0),
    };
    GameObject PowerUp;
    GameObject Cheatsheet;

    // Use this for initialization
    void Start ()
    {
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
