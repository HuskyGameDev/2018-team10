using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColliderOnStart : MonoBehaviour {
	[SerializeField] private int numVertices; 
	[SerializeField] private int[] avoidLayers;
	[SerializeField] private float maxDistance;
	private LineRenderer lines;
	private PolygonCollider2D polyCollider;

	// We do what Light Collider does, but only once on start.
	void Start () {
		Vector2[] directions = new Vector2[numVertices];
		int layerMask;

		lines = GetComponent<LineRenderer>();
		polyCollider = GetComponent<PolygonCollider2D>();
		
		if(lines != null){
			lines.positionCount = numVertices;
		}

		directions = new Vector2[numVertices];

		for(int i = 0; i < numVertices; i++){
			directions[i] = new Vector2(
				Mathf.Cos((float)i * (2 * Mathf.PI / (float)numVertices)), 
				Mathf.Sin((float)i * (2 * Mathf.PI / (float)numVertices))
			);
		}

		layerMask = (int)0x7FFFFFFF;

		for(int i = 0; i < avoidLayers.Length; i++){
			layerMask ^= (1 << avoidLayers[i]);
		}

		//Setup our poly collider and line renderers
		LightCollider.UpdateCollider(lines, polyCollider, transform, directions, layerMask, maxDistance);
	}
	
}
