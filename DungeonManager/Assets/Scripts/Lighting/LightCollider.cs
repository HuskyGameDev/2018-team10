using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour {
	[SerializeField] private int numVertices; 
	[SerializeField] private int[] avoidLayers;
	[SerializeField] private float torchMaxDistance;
	[SerializeField] private float noTorchMaxDistance;
	[SerializeField] private float growShrinkSpeed;
	[SerializeField] private IlluminationManager illuminationManager;
	private LineRenderer lines;
	private PolygonCollider2D polyCollider;
	private Vector2[] directions;
	private int layerMask;
	private float maxDistance;
	private float lastTime = 0;

	void Start () {
		//Line renderer and polygon collider are our friends
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

		maxDistance = noTorchMaxDistance;
	}
	
	void Update() {
		if(illuminationManager.illuminated){
			maxDistance = Mathf.Lerp(maxDistance, torchMaxDistance, growShrinkSpeed * Time.deltaTime);
		}else{
			maxDistance = Mathf.Lerp(maxDistance, noTorchMaxDistance, growShrinkSpeed * Time.deltaTime);
		}

		UpdateCollider(lines, polyCollider, transform, directions, layerMask, maxDistance);
	}

	public void UpdateCollider(){
		UpdateCollider(lines, polyCollider, transform, directions, layerMask, maxDistance);
	}

	public static void UpdateCollider(LineRenderer line, PolygonCollider2D poly, Transform transform, Vector2[] directions, int layerMask, float maxDistance){
		Vector3[] vecs = new Vector3[directions.Length];
		
		Matrix4x4 matLocalToWorld = transform.localToWorldMatrix;
		Matrix4x4 matWorldToLocal = /*Matrix4x4.Scale(new Vector3(1/transform.lossyScale.x, 1/transform.lossyScale.y, 1/transform.lossyScale.z)) **/ transform.worldToLocalMatrix;
		for(int i = 0; i < directions.Length; i++){
			//Find extents of each point
			RaycastHit2D hit = 
				Physics2D.Raycast(
					matLocalToWorld.MultiplyPoint(Vector3.zero), 
					directions[i],
					Mathf.Infinity,
					layerMask
				);

			if(hit.collider != null){
				vecs[i] = matWorldToLocal.MultiplyPoint(hit.point);
			}else{
				vecs[i] = maxDistance * directions[i];
			}
			vecs[i] = Vector3.ClampMagnitude(vecs[i], maxDistance);
		}
		//Draw a line using our points
		if(line != null) line.SetPositions(vecs);

		//Convert our lengths into vector2's
		Vector2[] vecs2D = new Vector2[vecs.Length];
		
		for(int i = 0; i < vecs.Length; i++){
			vecs2D[i] = new Vector2(vecs[i].x, vecs[i].y);
		}
		//Our collider is a polygon collider with the points we calculated
		poly.SetPath(0, vecs2D);
	}
/* 
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Torch"){
			Debug.Log("Child Torch enter");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Torch"){
			Debug.Log("Child Torch exit");
		}
	}
*/
}
