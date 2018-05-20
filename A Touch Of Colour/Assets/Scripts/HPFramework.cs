using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFramework : MonoBehaviour {

	public float hp; // Health of entity
	public GameObject deathAnimation; // Death particle effect
	
    // Function deals damage to entity
	public void Damage(float damage)
	{
		hp -= damage; // Subract damage from hp
		if (hp <= 0) // When hp is less than or equal to zero, entity dies
		{
			if (deathAnimation != null)
				Instantiate(deathAnimation, transform.position, Quaternion.identity); // Plays death animation
			Destroy(gameObject); // Destroys entity
		}
	}

}
