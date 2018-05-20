using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	public float speed; // Speed of the enemy
	public float damage; // Damage output of the enemy
	public float pushForce; // Strength of knockback
	public float pushStunTime; // How long to prevent player from moving after knockback

	public GameObject player; // Player
	PlayerController playerControls; // Player's variables (Mainly used to disable player movement)
	HPFramework playerHP; // Player's hp

	float pushTimer = 0; // Timer for the knockback stun

	Rigidbody2D rb; // Rigidbody2d of enemy

	ScreenShake shake;
	 
	private bool touching = false; // Determines whether to stand still or follow player

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>(); // Gets rigidbody2d component of enemy
		playerControls = player.GetComponent<PlayerController>(); // Gets PlayerController of player
		playerHP = player.GetComponent<HPFramework>(); // Gets player hp
		shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ScreenShake>(); 
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!touching && player != null) // Moves enemy towards player if touching = false
			rb.AddForce(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y) * speed, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject == player)
		{
			touching = true; // Prevents enemy from moving further
			StartCoroutine(PushTimerCountdown()); // Starts timer
			shake.ShakeScreen();
			Vector2 contact = col.contacts[0].point - new Vector2(player.transform.position.x, player.transform.position.y); // Contact push
			contact = -contact.normalized; // Normalizes the push force
			player.GetComponent<Rigidbody2D>().AddForce(contact * pushForce, ForceMode2D.Impulse); // Applies the push to the player
			playerHP.Damage(damage);
		}
	}


	/**
	 * This couroutine controls the timer of knockback 
	 * In essence it uses the push stun timer and sets it to the current pushtimer
	 * It then waits that many seconds before setting the timer back to zero (allowing both enemy and player to move)
	 */

	private IEnumerator PushTimerCountdown()
	{
		pushTimer = pushStunTime;
		playerControls.allowInput = false; ; // Disables player input
		while (pushTimer > 0)
		{
			//Debug.Log("Push Timer: " + pushTimer);
			yield return new WaitForSeconds(pushStunTime);
			pushTimer--;
			if (pushTimer <= 0) {
				// Re-enables player and enemy movement
				playerControls.allowInput = true;
				touching = false;
			}
		}
	}

	
	private void OnDestroy()
	{
		playerControls.allowInput = true;
	}

	/*
	void OnCollisionExit2D (Collision2D col)
	{
		if (col.gameObject == player)
		{
			touching = false;
		}
	}*/
}
