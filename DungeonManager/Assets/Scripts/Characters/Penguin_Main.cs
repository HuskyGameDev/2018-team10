using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Penguin_Main : MonoBehaviour {

    public int PenguinSpeed = 5;
    public int XMoveDirection = -1;
    public bool facingRight = false;
    public int jumpPower;
    private bool hasKey = false;
    private int layerMask;
    public int[] avoidLayers;

    private Animator anim;
    private bool atDoor = false;
    private Coroutine walkingSound = null;

    private void Start()
    {
        if(facingRight == true)
        {
            Flip();
        }

        layerMask = (int)0x7FFFFFFF;

        
        for (int i = 0; i < avoidLayers.Length; i++)
        {
            layerMask ^= (1 << avoidLayers[i]);
        }
        
        anim = gameObject.GetComponentInChildren<Animator>();

        // Lock movement and fade in
        StartCoroutine(FadeSprite.Fade(GetComponent<Rigidbody2D>(), gameObject.GetComponentInChildren<SpriteRenderer>(), 0f, 1f, 2f));
        //gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        
        walkingSound = StartCoroutine(PlayWalkingSoundForever());
    }

    void FixedUpdate(){
        if(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > 0.01 ){
            if(walkingSound != null){
                StopCoroutine(walkingSound);    
                walkingSound = null;
            }
        }else{
            if(walkingSound == null){
                walkingSound = StartCoroutine(PlayWalkingSoundForever());
            }
        }
    }

    // Update is called once per frame
    void Update () {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(GetComponent<BoxCollider2D>().size.x * 0.09f, GetComponent<BoxCollider2D>().size.y * 0.09f), 0 , new Vector2(XMoveDirection, 0), Mathf.Infinity, layerMask);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection * PenguinSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        if(hit.distance < 0.15f)
        {
            Flip();
        }
        
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 && !atDoor)
        {
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
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
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    //Check for a trigger and do correct action
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Check for death and die if true
        if(col.gameObject.tag == "Death")
        {
            Die(col);
            return;
        }
        //Check for trap and jump if found
        if(col.gameObject.tag == "Trap" && col.gameObject.GetComponentInParent<Trap_Trigger>().isLit)
        {
            Jump();
        }
        //Check for door and key then load next level if true
        if(col.gameObject.tag == "Door" && hasKey == true && col.gameObject.GetComponent<Door>().TorchesLit())
        {
            atDoor = true;
            GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
            StartCoroutine(LoadNextScene(col.gameObject.GetComponent<Animator>()));
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //Make the penguin jump
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
    }

    private IEnumerator DelayWwiseEvent(float time, string sound){
        yield return new WaitForSeconds(time);
        AkSoundEngine.PostEvent(sound, this.gameObject);
    }

    //Call when penguin dies to relaod scene
    void Die(Collider2D col)
    {
        StopCoroutine(walkingSound);
        StopAllCoroutines();

        if (col.gameObject.transform.parent.ToString().Substring(0,5).Equals("Spike"))
        {
            StartCoroutine(DelayWwiseEvent(0.5f, "Penguin_Death"));
            col.gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            anim.SetBool("Die_Spikes", true);
        }
        else
        {
            StartCoroutine(DelayWwiseEvent(0.8f, "Penguin_Meeps"));
            anim.SetBool("Die_Dennise", true);
        }

        
        //XMoveDirection = 0;
        GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(ReloadScene(1f));
    }

    public void SetHasKey(bool k)
    {
        hasKey = k;
    }

    public bool GetHasKey()
    {
        return hasKey;
    }

    // Waits for completed death animation and then for time seconds after
    // then reloads the scene
    IEnumerator ReloadScene(float time)
    {
        yield return new WaitUntil(() => IsDeathAnimation() && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Returns true if the current playing animation is a death animation
    private bool IsDeathAnimation()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Pengwin_explode") || anim.GetCurrentAnimatorStateInfo(0).IsName("Pengwin_spikes");
    }


    // Waits for the completed door animation and then loads the next scene
    IEnumerator LoadNextScene(Animator doorAnim)
    {
        yield return new WaitUntil(() => doorAnim.GetCurrentAnimatorStateInfo(0).IsName("Door_open") && doorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        StartCoroutine(FadeSprite.Fade(GetComponent<Rigidbody2D>(), gameObject.GetComponentInChildren<SpriteRenderer>(), 1f, 0f, 2f));
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Walking sound
    private bool playingWalkingSound = false;
    /*This coroutine plays the walking sound for as long as it's running*/
    private IEnumerator PlayWalkingSoundForever(){
        while(true){
            AkSoundEngine.PostEvent("Penguin_Flipper_Footstep", gameObject, (uint)AkCallbackType.AK_EndOfEvent, WalkingSoundEndCallback, this);
            playingWalkingSound = true;
            yield return new WaitWhile(() => playingWalkingSound);
        }
    }

    private void WalkingSoundEndCallback(object in_cookie, AkCallbackType in_type, object in_callbackInfo){
        playingWalkingSound = false;
    }

}
