using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCannonTurret : MonoBehaviour {

    public float speed;
    public Transform cannon;
	public Transform barrel;
    public Transform player;
    public float startShootingTime;
    public float fireRate;
	public TurretBullet bullet;
	public AudioClip turretSound;
	public Animator gameOver;
	AudioSource audio;

	private void Start()
	{
		audio = GetComponent<AudioSource>();
		InvokeRepeating("ShootBullet", startShootingTime, fireRate);
	}

	// Update is called once per frame
	void Update () {
		if (player != null)
		{
			Vector3 vectorToPlayer = player.position - cannon.position;
			float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			cannon.transform.rotation = Quaternion.Slerp(cannon.transform.rotation, q, Time.deltaTime * speed);
		}
		if (gameOver.GetCurrentAnimatorStateInfo(0).IsName("end"))
		{
			audio.enabled = false;
		}
	}

    void ShootBullet()
    {
		if (!gameOver.GetCurrentAnimatorStateInfo(0).IsName("end"))
		{
			TurretBullet newBullet = Instantiate(bullet, new Vector2(barrel.position.x, barrel.position.y), Quaternion.identity);
			audio.PlayOneShot(turretSound);
			newBullet.SetDirection(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y) * speed);
		}
		
	}

}
