using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

	#region Variables
	public float bulletSpeed; // Speed of the bullet
	public float fireRate; // Rate of fire of the bullet
	public float damage; // Damage of the bullet
	public GameObject regularEffect; // The bullet's death effect

    bool hit = false;

	AudioSource audio;
	public AudioClip hitSound;

    // Enemy hit effects
    public GameObject basicEnemyEffect; // Basic enemy hit effect

	Rigidbody2D rb; // Rigidbody of bullet
	private Vector2 moveVel; // Move velocity of the bullet

	#endregion

	#region Functions
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>(); // Gets rigidbody2d of bullet
		audio = GetComponent<AudioSource>();
	}

	// This function sets which direction the bullet will travel towards
	public void SetDirection(Vector2 input)
	{
		moveVel = input.normalized * bulletSpeed;
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.MovePosition(rb.position + moveVel * Time.fixedDeltaTime); // Moves the bullet
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != "Player" && !hit)
		{

            hit = true;

            // Bullet emits a particle effect then despawns
            // Only when it hits something that is not the player
            //Instantiate(effect, transform.position, Quaternion.identity);
            if (collision.GetComponent<BasicEnemy>() != null)
                Instantiate(basicEnemyEffect, transform.position, Quaternion.identity);
            else
                Instantiate(regularEffect, transform.position, Quaternion.identity);

			// Damages thing if it has an HPFramwork
			if (collision.gameObject.GetComponent<HPFramework>() != null)
			{
				collision.gameObject.GetComponent<HPFramework>().Damage(damage);
			}

			GetComponent<SpriteRenderer>().enabled = false;

			audio.PlayOneShot(hitSound);

			Destroy(gameObject, 1f); // Destroys bullet
		}

		if (collision.GetComponent<Flash>() != null)
		{
			collision.GetComponent<Flash>().Darken();
		}
	
	}

	#endregion

}
