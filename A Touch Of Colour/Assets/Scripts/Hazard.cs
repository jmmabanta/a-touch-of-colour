using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

	public float damage;
	public float pushForce;
	public float pushStunTime;

	GameObject player;
	PlayerController playerControls;
	HPFramework playerHP;

	float pushTimer = 0;

	ScreenShake shake;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerControls = player.GetComponent<PlayerController>();
		playerHP = player.GetComponent<HPFramework>();
		shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ScreenShake>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == player)
		{
			StartCoroutine(PushTimerCountdown());
			shake.ShakeScreen();
			Vector2 contact = collision.contacts[0].point - new Vector2(player.transform.position.x, player.transform.position.y); // Contact push
			contact = -contact.normalized; // Normalizes the push force
			player.GetComponent<Rigidbody2D>().AddForce(contact * pushForce, ForceMode2D.Impulse); // Applies the push to the player
			playerHP.Damage(damage);
		}
	}

	private IEnumerator PushTimerCountdown()
	{
		pushTimer = pushStunTime;
		playerControls.allowInput = false; ; // Disables player input
		while (pushTimer > 0)
		{
			//Debug.Log("Push Timer: " + pushTimer);
			yield return new WaitForSeconds(pushStunTime);
			pushTimer--;
			if (pushTimer <= 0)
			{
				// Re-enables player and enemy movement
				playerControls.allowInput = true;
			}
		}
	}

}
