using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour {

	public float dX;
	public float dY;
	public Light controlledLight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float x = controlledLight.transform.position.x;
		float y = controlledLight.transform.position.y;

		x += Input.GetAxis("Horizontal")*dX;
		y += Input.GetAxis("Vertical")*dY;

		controlledLight.transform.position = new Vector3(x, y, controlledLight.transform.position.z);
	}
}
