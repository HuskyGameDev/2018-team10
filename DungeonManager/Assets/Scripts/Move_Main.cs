using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Main : MonoBehaviour {

    public float playerSpeed = 5;
    public int playerJumpPower = 400;
    public float fallMult = 2.5f;
    public float lowJumpMult = 2f;

    private bool isGrounded = true;
    private bool facingRight = true;
    private Rigidbody2D body;
    private Animator animator;
    private GameObject heldItem;


    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        heldItem = null;
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlaceItem();
    }


    //Calls other methods for moving the character
    void PlayerMove()
    {
        //controls
        //Walk
        float moveX = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveX));

        //Jump
        Jump();
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
        body.velocity = new Vector2(moveX * playerSpeed, body.velocity.y);
    }

    //Let the player jump when player presses jump key
    // Changes velocity to jump and gravity for better
    // feeling fall and low jumping
    void Jump()
    {
        // Jump up
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            body.velocity += Vector2.up * playerJumpPower;
        }

        // Change player gravity for better jumping feel
        // Taken from YouTube:
        // Board To Games - Better Jumping in Unity with Four Lines of Code

        if (body.velocity.y < 0)                                        // Falling down
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))       // Going up but not holding jump
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
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


    private void PlaceItem()
    {
        if (Input.GetButtonDown("Pickup") && heldItem != null)
        {
            float dir = (facingRight ? 0.5f : -0.5f);
            heldItem.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + dir,
                gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            heldItem.SetActive(true);
            heldItem = null;

        }
    }


    /*void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }*/


    public void SetIsGrounded(bool b)
    {
        isGrounded = b;
    }


    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void SetHeldItem(GameObject h)
    {
        heldItem = h;
    }

    public GameObject GetHeldItem()
    {
        return heldItem;
    }
}
