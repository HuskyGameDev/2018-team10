using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCollider : MonoBehaviour {

    public Move_Main move;

    private void OnTriggerEnter2D(Collider2D other)
    {
        move.isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        move.isGrounded = false;
    }
}
