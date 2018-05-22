using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {

	// Go to main menu
	// TODO: Make main menu
	public Animator transition;

	public void MainMenu()
	{
		//Debug.Log("MAIN MENU!!! (not made yet)");
		StartCoroutine(LoadLevelTransition(0));
	}

	IEnumerator LoadLevelTransition(int sceneIndex)
	{
		transition.SetTrigger("end");
		yield return new WaitForSeconds(1.25f);
		SceneManager.LoadScene(sceneIndex);
	}

	// Quits Game
	public void QuitGame()
	{
		Application.Quit();
	}

}
