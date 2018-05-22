using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public List<EnemyTypes> enemyTypes;
	List<GameObject> spawners; 

	public int enemySpawnLimit;
	private int enemiesSpawned;

	public float startSpawningTime;
	public float spawnInterval;

	private void Start()
	{
		spawners = new List<GameObject>();

		foreach (Transform child in transform)
		{
			spawners.Add(child.gameObject);
			//Debug.Log(child.gameObject);
		}

		enemiesSpawned = 0;

		InvokeRepeating("SpawnEnemy", startSpawningTime, spawnInterval);
	}

	void SpawnEnemy()
	{
		if (enemiesSpawned < enemySpawnLimit)
		{
			for (int i = 0; i < enemyTypes.Count; i++)
			{
				if (Random.value * 100 <= enemyTypes[i].chance)
				{
					Instantiate(enemyTypes[i].enemy, spawners[Random.Range(0, spawners.Count)].transform.position, Quaternion.identity);
					enemiesSpawned++;
				}
			}
		}
	}

}

[System.Serializable]
public class EnemyTypes
{
	public GameObject enemy;
	public float chance;
}
