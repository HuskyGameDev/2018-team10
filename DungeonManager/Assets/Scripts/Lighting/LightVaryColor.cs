using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVaryColor : MonoBehaviour {
	private float curX = 0f;
	[SerializeField] private float speed;
	[SerializeField] private float sequence;
	[SerializeField] private Color color1;
	[SerializeField] private Color color2;
	private Light variedLight;
	// Use this for initialization
	void Start () {
		variedLight = GetComponent<Light>();	
	}
	
	// Update is called once per frame
	void Update () {
		curX += speed;
		variedLight.color = Color.Lerp(color1, color2, Mathf.PerlinNoise(curX, sequence));
	}
}
