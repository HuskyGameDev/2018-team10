using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {

    public int PenguinSpeed = 1;
    public int XMoveDirection = 1;
    public bool facingRight = true;
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * PenguinSpeed;
        if(hit.distance < 0.4f)
        {
            Flip();
        }
	}

    void Flip()
    {
        XMoveDirection *= -1;
        FlipPlayer();
    }

    //Flip the character sprite to match movement
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

   
}
