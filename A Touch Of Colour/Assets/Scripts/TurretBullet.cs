using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

	public float bulletSpeed;
	public float damage;
	public GameObject effect;
	public float pushForce;
	public float pushStunTime;

	GameObject player;

	float pushTimer = 0f;

	bool hit = false;

	AudioSource audio;
	public AudioClip hitSound;

	Rigidbody2D rb;
	private Vector2 moveVel;

	ScreenShake shake;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
		shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ScreenShake>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void SetDirection(Vector2 dir)
	{
		moveVel = dir.normalized * bulletSpeed;
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.MovePosition(rb.position + moveVel * Time.fixedDeltaTime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == player && !hit)
		{
			hit = true;
			Instantiate(effect, transform.position, Quaternion.identity);
			collision.gameObject.GetComponent<HPFramework>().Damage(damage);
			shake.ShakeScreen();
			Vector2 contact = collision.contacts[0].point - new Vector2(player.transform.position.x, player.transform.position.y); // Contact push
			contact = -contact.normalized; // Normalizes the push force
			player.GetComponent<Rigidbody2D>().AddForce(contact * pushForce, ForceMode2D.Impulse); // Applies the push to the player
			GetComponent<SpriteRenderer>().enabled = false;
			audio.PlayOneShot(hitSound);
			Destroy(gameObject, 1f);
		}

		if (collision.gameObject.tag == "Walls" || collision.gameObject.tag == "Bullet" && !hit)
		{
			hit = true;
			Instantiate(effect, transform.position, Quaternion.identity);
			GetComponent<SpriteRenderer>().enabled = false;
			audio.PlayOneShot(hitSound);
			Destroy(gameObject, 1f);
		}
	}

	private IEnumerator PushTimerCountdown()
	{
		pushTimer = pushStunTime;
		player.GetComponent<PlayerController>().allowInput = false;
		while (pushTimer > 0)
		{
			yield return new WaitForSeconds(pushStunTime);
			pushTimer--;
			if (pushTimer <= 0)
			{
				player.GetComponent<PlayerController>().allowInput = true;
			}
		}
	}

}
