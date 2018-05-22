using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Animator transition;

	public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadLevel(int sceneIndex)
	{
		//SceneManager.LoadScene(sceneIndex);
		StartCoroutine(LoadLevelTransition(sceneIndex));
	}

	IEnumerator LoadLevelTransition(int sceneIndex)
	{
		transition.SetTrigger("end");
		yield return new WaitForSeconds(1.25f);
		SceneManager.LoadScene(sceneIndex);
	}

}
