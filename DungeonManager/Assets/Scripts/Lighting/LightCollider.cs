using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour {
	[SerializeField] private int numVertices; 
	[SerializeField] private int[] avoidLayers;
	[SerializeField] private float maxDistance;
	private LineRenderer lines;
	private PolygonCollider2D polyCollider;
	private Vector2[] directions;
	private int layerMask;


	// Use this for initialization
	void Start () {
		lines = GetComponent<LineRenderer>();
		polyCollider = GetComponent<PolygonCollider2D>();

		lines.positionCount = numVertices;

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

	}
	
	// Update is called once per frame
	void Update () {
		Vector3[] vecs = new Vector3[numVertices];
		for(int i = 0; i < numVertices; i++){
			//Debug.Log("Directions[" + i + "] = (" + directions[i].x + ", " + directions[i].y + ")");
			RaycastHit2D hit = 
				Physics2D.Raycast(
					transform.localToWorldMatrix.MultiplyPoint(Vector3.zero), 
					directions[i],
					Mathf.Infinity,
					layerMask
				);
			//
			//Debug.Log("vecs[" + i + "] = " + hit.point);
			if(hit.collider != null){
				vecs[i] = transform.worldToLocalMatrix.MultiplyPoint(hit.point);
			}else{
				vecs[i] = maxDistance * directions[i];
			}
			vecs[i] = Vector3.ClampMagnitude(vecs[i], maxDistance);
		}

		lines.SetPositions(vecs);
		
		Vector2[] vecs2D = new Vector2[numVertices];
		
		for(int i = 0; i < numVertices; i++){
			vecs2D[i] = new Vector2(vecs[i].x, vecs[i].y);
		}

		polyCollider.SetPath(0, vecs2D);
	}
}
