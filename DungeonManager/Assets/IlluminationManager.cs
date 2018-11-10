using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminationManager : MonoBehaviour {
	public bool illuminated = false;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Torch"){
			illuminated = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Torch"){
			illuminated = false;
		}
	}
}
