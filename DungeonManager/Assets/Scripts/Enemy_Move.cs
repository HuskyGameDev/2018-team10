using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {

    public int EnemySpeed = 1;
    public int XMoveDirection = 1;
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
        if(hit.distance < 0.3f)
        {
            Flip();
        }
	}

    void Flip()
    {
        XMoveDirection *= -1;
    }
}
