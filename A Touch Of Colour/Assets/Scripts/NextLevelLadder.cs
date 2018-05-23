using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLadder : MonoBehaviour {

	public Animator transition;
	public int sceneIndex;

	bool levelDone = false;

	public EnemySpawner spawner;
	public PlayerController player;

	public GameObject turret;

	Animator ladder;

	private void Start()
	{
		ladder = GetComponent<Animator>();
	}

	private void Update()
	{
		if (player.kills >= spawner.enemySpawnLimit && !levelDone)
		{
			if (turret == null)
			{
				levelDone = true;
				ladder.SetTrigger("SpawnIn");
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == player.gameObject && levelDone)
		{
			//Debug.Log("HI");
			StartCoroutine(nextLevel(sceneIndex));
		}
	}

	IEnumerator nextLevel(int sceneIndex)
	{
		transition.SetTrigger("end");
		yield return new WaitForSeconds(1.25f);
		SceneManager.LoadScene(sceneIndex);
	}

}
