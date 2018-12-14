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
        toggledLight.gameObject.SetActive(isLit);
        input = GameObject.FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (playerNear && input.GetButtonDownUnpaused("Light"))
        {
            SetLight(!isLit);
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

    public void SetLight(bool state)
    {
        isLit = state;
        toggledLight.gameObject.SetActive(state);
        if (isLit)
        {
            AkSoundEngine.PostEvent("Denis_Lights_Torch", this.gameObject);
        }
    }
}
