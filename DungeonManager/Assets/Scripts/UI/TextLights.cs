using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextLights : MonoBehaviour {

    public Torch_Toggle[] lights;
    private Coroutine nextLightCoroutine;
    private int index = 0;

	// Use this for initialization
	void Start () {
        nextLightCoroutine = StartCoroutine(NextLight(0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
        {
            StopCoroutine(nextLightCoroutine);
            LightTorches();
        }
	}


    // Wait for time and then light
    IEnumerator NextLight(float time)
    {
        yield return new WaitForSeconds(time);

        LightTorches();
    }


    // Wait for time and load next scene
    IEnumerator LoadNextScene(float time)
    {
        yield return new WaitForSeconds(time);
        LightTorches();
    }


    // Lights torches and starts next coroutine
    void LightTorches()
    {
        if (index < lights.Length)
        {
            lights[index].SetLight(true);
            lights[index + 1].SetLight(true);
            index = index + 2;
            if (index < lights.Length)
            {
                nextLightCoroutine = StartCoroutine(NextLight(4f));
            }
            else
            {
                nextLightCoroutine = StartCoroutine(LoadNextScene(10f));
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals("end"))
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
