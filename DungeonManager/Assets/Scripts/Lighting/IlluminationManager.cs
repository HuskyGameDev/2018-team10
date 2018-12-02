using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminationManager : MonoBehaviour {
	public bool illuminated = false;
	public Torch_Toggle torch = null;

	void FixedUpdate(){
		if(torch != null){
			if(torch.isLit){
				illuminated = true;
			}else{
				illuminated = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Torch"){
			if(other.gameObject.GetComponent<Torch_Toggle>().isLit){
				illuminated = true;
			}
			torch = other.gameObject.GetComponent<Torch_Toggle>();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Torch"){
			illuminated = false;
			torch = null;
		}
	}
}
