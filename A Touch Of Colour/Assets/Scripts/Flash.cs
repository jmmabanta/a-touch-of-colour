using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

	public float darkness;
	public float timing;

	SpriteRenderer sprite;
	
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	public void Darken()
	{
		StartCoroutine(DarkSprite());
	}

	private IEnumerator DarkSprite()
	{
		bool flashed = false;
		while (!flashed)
		{
			Color change = Color.Lerp(Color.white, new Color(darkness, darkness, darkness), Time.time);
			sprite.color = change;
			yield return new WaitForSeconds(timing);
			change = Color.Lerp(new Color(darkness, darkness, darkness), Color.white, Time.time);
			sprite.color = change;
			flashed = true;
			yield return new WaitForSeconds(timing);
		}
	}

}
