using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTorch : MonoBehaviour {
	[SerializeField] private SpriteRenderer follow;
	private bool isRight;
	// Use this for initialization
	void Start () {
		isRight = transform.localPosition.x > 0;
	}
	
	// Update is called once per frame
	void Update () {
		if((isRight  && follow.flipX) ||
		   (!isRight && !follow.flipX)){
			isRight = !isRight;
			transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		}
	}
}
