using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

	public float darkness; // How dark the object will get
	public float timing; // Time it takes to darken

	SpriteRenderer sprite; // Sprite to darken
	
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>(); // Gets the SpriteRenderer
	}

    // Function flashes the sprite
	public void Darken()
	{
		StartCoroutine(DarkSprite());
	}

	private IEnumerator DarkSprite()
	{
		bool flashed = false;
		while (!flashed)
		{
            Color change = new Color(darkness, darkness, darkness);
			sprite.color = change;
			yield return new WaitForSeconds(timing);
            change = Color.white;
			sprite.color = change;
			flashed = true;
			yield return new WaitForSeconds(timing);
		}
	}

}
