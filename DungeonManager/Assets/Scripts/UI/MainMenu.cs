using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	void OnEnable(){
		StartCoroutine(DelayWwiseEvent(1, "Whip_Crack"));
        foreach (Button b in gameObject.GetComponentsInChildren<Button>())
        {
            b.interactable = false;
            StartCoroutine(EnableButton(b));
        }
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

    private IEnumerator EnableButton(Button b)
    {
        yield return new WaitUntil(() => gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Sway"));
        b.interactable = true;
    }
}
