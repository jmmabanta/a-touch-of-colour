using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	public Animator camera;

	public void ShakeScreen()
	{
		int random = Random.Range(1, 4);
		switch (random)
		{
			case 1:
				camera.SetTrigger("Shake1");
				break;
			case 2:
				camera.SetTrigger("Shake2");
				break;
			case 3:
				camera.SetTrigger("Shake3");
				break;
			default:
				break;
		}
		 //Debug.Log(random);
	}

}
