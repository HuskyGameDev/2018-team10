using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Toggle : MonoBehaviour {

    public bool isLit;
    private bool playerNear = false;
    public Light toggledLight;

    private InputManager input;

    void Start()
    {
        SetLight(isLit);
        input = GameObject.FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (playerNear && input.GetButtonDownUnpaused("Light"))
        {
            isLit = !isLit;
            SetLight(isLit);
            if(isLit){
                AkSoundEngine.PostEvent("Denis_Lights_Torch", this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = false;
        }
    }

    void SetLight(bool state)
    {
        toggledLight.gameObject.SetActive(state);
    }
}
