using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeMenu : MonoBehaviour {

    public bool CopHumanController = false;
	public bool RocketBlitz = false;

    public static MapController.Car[] cars;

    public int selectedCarIndex = 0;
    public MapController.Car selectedCar;
	public MapController.Car policeCar;

    public void Start()
    {
        cars = new MapController.Car[] {
			new MapController.Car(Resources.Load<Sprite>("Cars/Sports"), 5000f, 0.4f, 0.2f, 2f,   750f,  1f),
			new MapController.Car(Resources.Load<Sprite>("Cars/Muscle"), 4000f, 0.6f, 0.4f, 1.5f, 1000f, 1f),
			new MapController.Car(Resources.Load<Sprite>("Cars/Pickup"), 3600f, 0.8f, 0.7f, 1f,   1500f, 1f),
			new MapController.Car(Resources.Load<Sprite>("Cars/Truck"),  2400f, 1.0f, 1.0f, 0.8f, 8000f, 1f)
    	};
		policeCar = new MapController.Car (Resources.Load<Sprite> ("Cars/Police1"), 4200f, 0.7f, 0.5f, 1f, 2000f, 1f);
        selectedCar = cars[selectedCarIndex];
    }

    public void PlaySinglePlayerGame(){
		MapController.CopHumanController = false;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void PlayMultiPlayerGame(){
		MapController.CopHumanController = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void PlayRocketBlitzGame() {
		MapController.CopHumanController = true;
		RocketBlitz = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

    public void SetCopHumanController(bool bl)
    {
        CopHumanController = bl;
    }

    public void SetSelectedCar(int sc)
    {
        selectedCarIndex = sc;
        selectedCar = cars[selectedCarIndex];
    }

	public void SetRocketBlitz(bool bl)
	{
		RocketBlitz = true;
	}

    public void StartGame()
    {
        MapController.CopHumanController = CopHumanController;
        MapController.selectedCar = selectedCar;
		MapController.policeCar = policeCar;
		MapController.rocketBlitz = RocketBlitz;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}