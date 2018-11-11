using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCollider : MonoBehaviour {

    public Move_Main move;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            move.SetIsGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            move.SetIsGrounded(false);
        }
    }
}
