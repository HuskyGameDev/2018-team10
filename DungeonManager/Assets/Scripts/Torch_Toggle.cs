using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Toggle : MonoBehaviour {

    public bool isLit;
    private bool playerNear = false;
    public Light toggledLight;


    void Start()
    {
        SetLight(isLit);
    }

    private void Update()
    {
        if (playerNear && Input.GetButtonDown("Light"))
        {
            isLit = !isLit;
            SetLight(isLit);
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
