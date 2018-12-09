using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {
	void Start(){
		StartCoroutine(DelayWwiseEvent(1, "Whip_Crack"));
	}

	private IEnumerator DelayWwiseEvent(float time, string sound){
        yield return new WaitForSeconds(time);
        AkSoundEngine.PostEvent(sound, this.gameObject);
    }

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
