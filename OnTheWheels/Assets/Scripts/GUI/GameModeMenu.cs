using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeMenu : MonoBehaviour {

	public void PlaySinglePlayerGame(){
		MapController.CopHumanController = false;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void PlayMultiPlayerGame(){
		MapController.CopHumanController = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}