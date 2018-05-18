using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFramework : MonoBehaviour {

	public float hp; // Health of entity
	public GameObject deathAnimation;
	
	public void Damage(float damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			if (deathAnimation != null)
				Instantiate(deathAnimation, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

}
