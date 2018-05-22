using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5f; // Player's speed
	public BasicBullet basicBullet; // Basic bullet prefab
	public bool isFiring = false; // Determines if weapon can be fired
	public bool allowInput = true; // Determines if player can move

	private AudioSource audio;
	public AudioClip basicWeaponShootSound;

	private int kills = 0;
	public int Kills
	{
		get
		{
			return this.kills;
		}
		set
		{
			this.kills = value;
		}
	}


	float bulletCounter; // Keeps track of fire rate

	Rigidbody2D rb; // Player's rigidbody2d
	Vector2 moveVel; // Player's movement

	public enum Weapons // Lists all weapons player can use
	{
		StarterWeapon,
		Placeholder1,
		Placeholder2,
	};

	public Weapons weapon; // Current weapon

	public int Kills1
	{
		get
		{
			return kills;
		}

		set
		{
			kills = value;
		}
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>(); // Gets rigidbody2d of player
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 moveInput = new Vector2(); // Vector2 contains all movement input 

		if (allowInput)
			moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Gets input

		// Following 2 if statements determines if the arrow keys are pressed (to shoot)
		if (Input.GetAxisRaw("ShootH") != 0 || Input.GetAxisRaw("ShootV") != 0)
		{
			isFiring = true;
		} 
		if (Input.GetAxisRaw("ShootH") == 0 && Input.GetAxisRaw("ShootV") == 0)
		{
			isFiring = false;
		}

		bulletCounter -= Time.deltaTime; // Counts down the bullet timer
		if (isFiring)
		{
			if (bulletCounter <= 0) // Fire weapon if the timer is less than or equal 0
			{
				switch (weapon) // Possible weapons to fire
				{
					case Weapons.StarterWeapon:
						bulletCounter = basicBullet.fireRate; // Sets timer to weapon's fire rate
						audio.clip = basicWeaponShootSound;
						audio.Play();
						BasicBullet newBullet = Instantiate(basicBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as BasicBullet; // Creates the bullet
						newBullet.SetDirection(new Vector2(Input.GetAxisRaw("ShootH"), Input.GetAxisRaw("ShootV"))); // Sets bullet direction to arrow key direction
						break;
					case Weapons.Placeholder1:
						break;
					case Weapons.Placeholder2:
						break;
					default:
						break;
				}
			}
		}
			

		moveVel = moveInput * speed; // Input * Speed
	}

	private void FixedUpdate()
	{
		rb.AddForce(moveVel, ForceMode2D.Impulse); // Moves player
	}

}
