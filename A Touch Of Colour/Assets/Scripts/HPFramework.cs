using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFramework : MonoBehaviour {

	public float hp; // Health of entity
	public GameObject deathAnimation; // Death particle effect

	public bool hasSound;

	public AudioClip hitSound;
	AudioSource audio;

	[SerializeField]
	private Animator gameOver; // Game over animation (for player)

	private void Start()
	{	
		if (hasSound)
			audio = GetComponent<AudioSource>();
	}

	// Function deals damage to entity
	public void Damage(float damage)
	{
		hp -= damage; // Subract damage from hp
		if (hitSound != null && audio != null)
		{
			audio.PlayOneShot(hitSound);
		}
		if (hp <= 0) // When hp is less than or equal to zero, entity dies
		{
			if (deathAnimation != null)
				Instantiate(deathAnimation, transform.position, Quaternion.identity); // Plays death animation
			if (gameObject.tag == "Player")
			{
				gameOver.SetTrigger("GameOver");
			}

			if (hasSound && hitSound != null && audio != null)
			{
				GetComponent<SpriteRenderer>().enabled = false;
				GetComponent<Collider2D>().enabled = false;
				GetComponent<PlayerController>().enabled = false;
				audio.PlayOneShot(hitSound);
				Destroy(gameObject, 1f);
			} else
				Destroy(gameObject); // Destroys entity
		}
	}

}
