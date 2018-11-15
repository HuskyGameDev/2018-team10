using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    private bool playerNear = false;
    public Move_Main dennice;
    public Penguin_Main pengwin;
    public bool penguinCanPickup;

    private InputManager input;

    void Start(){
        input = GameObject.FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (playerNear && input.GetButtonDownUnpaused("Pickup"))
        {
            if (dennice.GetHeldItem() == null)
            {
                dennice.SetHeldItem(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = true;
        }
        if (other.gameObject.tag == "Penguin" && penguinCanPickup == false)
        {
            pengwin.SetHasKey(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = false;
        }
    }
    
}
