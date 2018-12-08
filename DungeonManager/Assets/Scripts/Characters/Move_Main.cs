using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Main : MonoBehaviour {

    public float playerSpeed = 5;
    public int playerJumpPower = 400;
    public float fallMult = 2.5f;
    public float lowJumpMult = 2f;
    [SerializeField] float pickupDelayTime;
    [SerializeField] float stepDelayTime;

    private bool isGrounded = true;
    private bool facingRight = true;
    private Rigidbody2D body;
    private Animator animator;
    private GameObject heldItem;
    private InputManager input;
    public bool canPickupItem = true;
    [SerializeField] private LightCollider lightCollider;
    private int layerMask;
    public int[] avoidLayers;

    public bool canPlayFootstep = true;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        heldItem = null;
        input = GameObject.FindObjectOfType<InputManager>();


        layerMask = (int)0x7FFFFFFF;


        for (int i = 0; i < avoidLayers.Length; i++)
        {
            layerMask ^= (1 << avoidLayers[i]);
        }

        // Lock movement and fade in
        StartCoroutine(FadeSprite.Fade(GetComponent<Rigidbody2D>(), gameObject.GetComponentInChildren<SpriteRenderer>(), 0f, 1f, 2f));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Call jump method
        Jump();
        //Check how the player is moving
        PlayerMove();
        //Check to see if the player is placing an item
        PlaceItem();
    }


    //Calls other methods for moving the character
    void PlayerMove()
    {
        //controls
        //Walk
        float moveX = input.GetAxisUnpaused("Horizontal");
        
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        
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

        //Step soundd
        if(Mathf.Abs(moveX) > 0.1f){
            Step();
        }

    }

    //Let the player jump when player presses jump key
    // Changes velocity to jump and gravity for better
    // feeling fall and low jumping
    void Jump()
    {
        // Jump up
        if (OnGround() && Input.GetButton("Jump"))
        {
            body.velocity = Vector2.up * playerJumpPower;      
        }
        
        
        
        
        // Change player gravity for better jumping feel
        // Taken from YouTube:
        // Board To Games - Better Jumping in Unity with Four Lines of Code

        
        if (body.velocity.y < 0)                                        // Falling down
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !input.GetButtonDownUnpaused("Jump"))       // Going up but not holding jump
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
        }
        
        
    }

    //Flip the character sprite to match movement
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        lightCollider.UpdateCollider();
    }

    private IEnumerator PickupDelay(){
        yield return new WaitForSeconds(pickupDelayTime);
        this.canPickupItem = true;
    }

    public void StartPickupDelay(){
        canPickupItem = false;
        StartCoroutine(PickupDelay());
    }
    //check if player has an item and places it if they do
    //translates item to be next to player frfom where it became inactive
    private void PlaceItem()
    {
        if (input.GetButtonDownUnpaused("Pickup") && heldItem != null && canPickupItem)
        {    
            StartPickupDelay();

            float dir = (facingRight ? 0.5f : -0.5f);
            heldItem.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + dir,
                gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            heldItem.SetActive(true);
            heldItem = null;
        }
    }

    //Footstep stuff
    private IEnumerator StepDelay(){
        yield return new WaitForSeconds(stepDelayTime);
        canPlayFootstep = true;
    }

    private void StartStepDelay(){
        canPlayFootstep = false;
        StartCoroutine(StepDelay());
    }

    private void Step(){
        if(canPlayFootstep && OnGround()){
            AkSoundEngine.PostEvent("Denis_Footsteps", this.gameObject);
            StartStepDelay();
        }
    }

    /*void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }*/

    public bool OnGround(){
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(GetComponent<BoxCollider2D>().size.x * 0.01f, GetComponent<BoxCollider2D>().size.y * 0.00009f), 0, Vector2.down, Mathf.Infinity, layerMask);
        return hit.distance < 0.65f;
    }

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
