using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denise : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("Horizontal");

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * 5, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
        }
    }
}
