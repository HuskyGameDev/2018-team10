using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {
	//loads in scenes  
	public void PlayGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	//Handles quit of the game and sends degub for testing
	public void QuitGame(){
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
