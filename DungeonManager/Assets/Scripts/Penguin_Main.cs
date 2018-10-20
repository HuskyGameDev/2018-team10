using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Penguin_Main : MonoBehaviour {

    public int PenguinSpeed = 5;
    public int XMoveDirection = 1;
    public bool facingRight = true;
    public int jumpPower;
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection * PenguinSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y) ;
        if(hit.distance < 0.3f)
        {
            Flip();
        }
	}

    //
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

    //Check for a trigger and do correct action
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Trap")
        {
            Jump();
        }
        if(col.gameObject.tag == "Death")
        {
            Die();
        }
    }

    //Make the penguin jump
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        //gameObject.GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpPower;
    }

    //Call when penguin dies to relaod scene
    void Die()
    {
        SceneManager.LoadScene("TheSceneFullOfPengUINZandfun");
    }
}
