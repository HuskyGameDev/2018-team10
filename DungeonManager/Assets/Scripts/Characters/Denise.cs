using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denise : MonoBehaviour {
	
    private InputManager input;

    void Start(){
        input = GameObject.FindObjectOfType<InputManager>();
    }

	// Update is called once per frame
	void Update () {
        float moveX = input.GetAxisUnpaused("Horizontal");

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * 5, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        if (input.GetButtonDownUnpaused("Jump"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
        }
    }
}
