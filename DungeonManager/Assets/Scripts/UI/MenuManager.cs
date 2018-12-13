using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour {

    public VideoPlayer player;
    public GameObject music;
    public GameObject menu;
    private bool playing = false;
	
	// Update is called once per frame
	void Start () {
        StartCoroutine(PlayTrailer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StopAllCoroutines();
            
            if (playing)
            {
                player.Stop();
                playing = false;
                AkSoundEngine.PostEvent("Main_Menu_Music", music);
            }
            StartCoroutine(PlayTrailer());
        }
    }

    public IEnumerator PlayTrailer()
    {
        yield return new WaitForSeconds(30f);
        playing = true;
        player.Play();
        AkSoundEngine.PostEvent("Main_Menu_Music_Off", music);
        while (player.isPlaying)
        {
            yield return null;
        }
        player.Stop();
        playing = false;
        AkSoundEngine.PostEvent("Main_Menu_Music", music);
        StartCoroutine(PlayTrailer());
    }
}
