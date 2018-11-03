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

    private void Start()
    {
        layerMask = (int)0x7FFFFFFF;

        for (int i = 0; i < avoidLayers.Length; i++)
        {
            layerMask ^= (1 << avoidLayers[i]);
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

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("Idle", false);
        }
        else
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("Idle", true);
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
        //Check for trap and jump if found
        if(col.gameObject.tag == "Trap" && col.gameObject.GetComponentInParent<Trap_Trigger>().isLit)
        {
            Jump();
        }
        //Check for death and die if true
        if(col.gameObject.tag == "Death")
        {
            Die();
        }
        //Check for door and key then load next level if true
        if(col.gameObject.tag == "Door" && hasKey == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //Make the penguin jump
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
    }

    //Call when penguin dies to relaod scene
    void Die()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.GetComponentInChildren<Animator>().SetBool("Die_Dennise", true);
    }

    public void SetHasKey(bool k)
    {
        hasKey = k;
    }

    public bool GetHasKey()
    {
        return hasKey;
    }
}
