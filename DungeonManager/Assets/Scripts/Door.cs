using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool isExit;

	// Use this for initialization
	void Start () {
        // Enter doors open at start
		if (!isExit)
        {
            gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Penguin" && col.gameObject.GetComponent<Penguin_Main>().GetHasKey() == true)
        {
            gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
        }
    }
}
