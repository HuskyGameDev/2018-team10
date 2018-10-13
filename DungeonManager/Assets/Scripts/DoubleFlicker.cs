using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFlicker : MonoBehaviour {
	private float curX = 0f;
	[SerializeField] private float speed;
	[SerializeField] private float sequence;

	[SerializeField] private float light1MinIntensity;
	[SerializeField] private float light2MinIntensity;
	[SerializeField] private float light1Variance;
	[SerializeField] private float light2Variance;

	[SerializeField] private Light light1;
	[SerializeField] private Light light2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		curX += speed;
		light1.intensity = light1MinIntensity + Mathf.PerlinNoise(curX, sequence) * light1Variance;
		light2.intensity = light2MinIntensity + (1 - Mathf.PerlinNoise(curX, sequence)) * light2Variance;
	}
}
