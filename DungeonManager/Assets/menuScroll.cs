using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScroll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(FadeSprite.FadeImage(gameObject.GetComponentInChildren<SpriteRenderer>(), 0f, 0f, 0.2f));
	 	StartCoroutine(waitForAnim());

	}
public IEnumerator waitForAnim(){
	yield return new WaitForSeconds(2.5f);
	StartCoroutine(FadeSprite.FadeImage(gameObject.GetComponentInChildren<SpriteRenderer>(), 0f, 1f, 1f));
}	
}
