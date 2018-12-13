using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSword : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (EmployeesMenu.complete == true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
	}
	
}
