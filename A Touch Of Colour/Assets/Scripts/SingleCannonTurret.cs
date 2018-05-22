using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCannonTurret : MonoBehaviour {

    public float speed;
    public Transform cannon;
    public Transform player;
    public float startShootingTime;
    public float fireRate;
	
	// Update is called once per frame
	void Update () {
        Vector3 vectorToPlayer = player.position - cannon.position;
        float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        cannon.transform.rotation = Quaternion.Slerp(cannon.transform.rotation, q, Time.deltaTime * speed);
    }

    void ShootBullet()
    {
        
    }

}
