using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
	private float curX = 0f;
	[SerializeField] private float speed;
	[SerializeField] private float sequence;
	[SerializeField] private float minIntensity;
	[SerializeField] private float variance;
	private Light lightToFlicker;
	// Use this for initialization
	void Start () {
		lightToFlicker = GetComponent<Light>();	
	}
	
	// Update is called once per frame
	void Update () {
		curX += speed;
		lightToFlicker.intensity = minIntensity + Mathf.PerlinNoise(curX, sequence) * variance;
	}
}
