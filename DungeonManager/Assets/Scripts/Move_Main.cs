using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Main : MonoBehaviour {

    public int playerSpeed = 5;
    public bool facingRight = true;
    public int playerJumpPower = 800;
    public float moveX;
    public bool inAir;


    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }


    //Calls other methods for moving the character
    void PlayerMove()
    {
        //controls
        //Walk
        moveX = Input.GetAxis("Horizontal");
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //animations
        //player direction
        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    //Let the player jump when player presses jump key
    //Add force to player to jump
    void Jump()
    {
        if(inAir == false)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
            inAir = true;
        }
    }

    //Flip the character sprite to match movement
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            inAir = false;
        }
    }
}
