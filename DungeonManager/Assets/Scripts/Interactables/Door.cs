using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float waitTime;
    public bool isExit;
    public Torch_Toggle torch1, torch2;
    public GameObject fadeObj;

	// Use this for initialization
	void Start () {
        if (fadeObj != null)
        {
            StartCoroutine(StartPengwin());
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Penguin" && TorchesLit() &&
            col.gameObject.GetComponent<Penguin_Main>().GetHasKey() == true)
        {
            StartCoroutine(EndPengwin());
        }
    }


    // Returns true if both torches are lit
    public bool TorchesLit()
    {
        if (isExit == true)
        {
            return torch1.isLit && torch2.isLit;
        }
        return false;
        
    }


    // After a certain amount of time, the door opens and the pengwin is active
    IEnumerator StartPengwin()
    {
        //yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f + waitTime);
        gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
        yield return new WaitUntil(() => gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Door_open") &&
                                         gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        fadeObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().SetBool("OpenDoor", false);
    }


    // After a certain amount of time, the door opens and the pengwin is active
    IEnumerator EndPengwin()
    {
        //yield return new WaitForSeconds(1f);
        //yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
        yield return new WaitUntil(() => gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Door_open") &&
                                         gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().SetBool("OpenDoor", false);
    }
}
