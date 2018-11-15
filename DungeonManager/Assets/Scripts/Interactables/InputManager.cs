using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	PauseMenu pause;

	public void Start(){
		pause = GetComponent<PauseMenu>();
	}

	public bool GetButtonDownUnpaused(string name){
		return !pause.IsPaused() && Input.GetButtonDown(name);
	}

	public bool GetButtonUpUnpaused(string name){
		return !pause.IsPaused() && Input.GetButtonUp(name);
	}

	public static bool GetButtonUp(string name){
		return Input.GetButtonUp(name);
	}

	public static bool GetButtonDown(string name){
		return Input.GetButtonDown(name);
	}

	public float GetAxisUnpaused(string name){
		return pause.IsPaused() ? 0 : Input.GetAxis(name);
	}

	public static float GetAxis(string name){
		return Input.GetAxis(name);
	}

}
