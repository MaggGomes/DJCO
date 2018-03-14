using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeMenu : MonoBehaviour {

    public bool CopHumanController = false;

    public static MapController.Car[] cars;

    public int selectedCarIndex = 0;
    public MapController.Car selectedCar;

    public void Start()
    {
        cars = new MapController.Car[]{
        new MapController.Car(Resources.Load<Sprite>("Cars/1"), 2000f, 0.7f, 0.4f, 1f, 1000f,   1f),
        new MapController.Car(Resources.Load<Sprite>("Cars/2"), 2000f, 0.7f, 0.4f, 1f, 1000f,   1f),
        new MapController.Car(Resources.Load<Sprite>("Cars/3"), 2000f, 0.7f, 0.4f, 1f, 1000f,   1f),
        new MapController.Car(Resources.Load<Sprite>("Cars/4"), 2000f, 0.7f, 0.4f, 1f, 1000f,   1f),
        new MapController.Car(Resources.Load<Sprite>("Cars/5"), 2000f, 0.7f, 0.4f, 1f, 1000f,   1f),
        new MapController.Car(Resources.Load<Sprite>("Cars/6"), 2000f, 0.7f, 0.4f, 1f, 1000f,   1f)
    };
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

    public void SetCopHumanController(bool bl)
    {
        CopHumanController = bl;
    }

    public void SetSelectedCar(int sc)
    {
        selectedCarIndex = sc;
        selectedCar = cars[selectedCarIndex];
    }


    public void StartGame()
    {
        MapController.CopHumanController = CopHumanController;
        MapController.selectedCar = selectedCar;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}