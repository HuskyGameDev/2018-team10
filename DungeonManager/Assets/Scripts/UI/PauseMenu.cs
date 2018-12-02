using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour {
	private bool paused = false;
	private bool pausePressed = false;

	private InputManager input;

	[SerializeField] private GameObject canvasObject;

	private static PauseMenu self;
	void Start(){
		input = GameObject.FindObjectOfType<InputManager>();
		self = this;
	}

	// Update is called once per frame
	void Update () {
		if(InputManager.GetButtonDown("Pause") && !pausePressed){
			pausePressed = true;
			
			if(paused) Unpause();
			else Pause();

		}else if(InputManager.GetButtonUp("Pause")){
			pausePressed = false;
		}
	}

	public bool IsPaused(){
		return paused;
	}

	public void Unpause(){
		canvasObject.SetActive(false);
		paused = false;
		Time.timeScale = 1;
	}

	public void Pause(){
		canvasObject.SetActive(true);
		paused = true;
		Time.timeScale = 0;
	}
	//WARNING: DUMBEST HACK OF ALL TIME TO MAKE UNITY CALL UNPAUSE CORRECTLY.
	//MODIFY/USE AT YOUR OWN RISK
	public void UnpauseStatic() {
		self.Unpause();
	}

	public void ToMainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
