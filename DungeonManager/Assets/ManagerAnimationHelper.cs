using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnimationHelper : MonoBehaviour {
	public void AnimDone()
	{
		gameObject.GetComponentInParent<MainMenuAnimation>().TitleShake();
	}
}
