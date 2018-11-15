using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour {
	private Animator title;
	private Animator manager;

	// Use this for initialization
	void Start () {
		Animator[] anims = gameObject.GetComponentsInChildren<Animator>();

		for (int i = 0; i < anims.Length; i++)
		{
			string name = anims[i].gameObject.name;
			//Debug.Log(anims[i].gameObject.name);
			if (name.Equals("Title_obj"))
			{
				title = anims[i];
			}
			if (name.Equals("Manager_obj"))
			{
				manager = anims[i];
			}

		}
		//title = GameComponentsInChildren
	}
	
	// Update is called once per frame
	void Update () {
		//manager.GetCurrentAnimatorStateInfo(0).IsName("manager_1") && manager.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
	}


	public void TitleShake()
	{
		title.SetBool("Shake", true);
	}
}
