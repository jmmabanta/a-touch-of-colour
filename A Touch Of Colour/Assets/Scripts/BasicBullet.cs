using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

	public float bulletSpeed; // Speed of the bullet
	public float fireRate; // Rate of fire of the bullet
	public float damage; // Damage of the bullet
	public GameObject effect; // The bullet's death effect

	Rigidbody2D rb; // Rigidbody of bullet
	private Vector2 moveVel; // Move velocity of the bullet

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>(); // Gets rigidbody2d of bullet
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
		if (collision.tag != "Player")
		{
			// Bullet emits a particle effect then despawns
			// Only when it hits something that is not the player
			Instantiate(effect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

		if (collision.GetComponent<Flash>() != null)
		{
			collision.GetComponent<Flash>().Darken();
		}

		if (collision.gameObject.GetComponent<HPFramework>() != null)
		{
			collision.gameObject.GetComponent<HPFramework>().Damage(damage);
		}

	}

}
