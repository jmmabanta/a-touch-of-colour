using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFramework : MonoBehaviour {

	public float hp; // Health of entity
	public GameObject deathAnimation; // Death particle effect

	[SerializeField]
	private Animator gameOver; // Game over animation (for player)
	
    // Function deals damage to entity
	public void Damage(float damage)
	{
		hp -= damage; // Subract damage from hp
		if (hp <= 0) // When hp is less than or equal to zero, entity dies
		{
			if (deathAnimation != null)
				Instantiate(deathAnimation, transform.position, Quaternion.identity); // Plays death animation
			if (gameObject.tag == "Player")
			{
				gameOver.SetTrigger("GameOver");
			}
			Destroy(gameObject); // Destroys entity
		}
	}

}
