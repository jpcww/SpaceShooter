using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour 
{
	//Reference for Star
	public GameObject starPrefab;
	//Maximum Number of Stars
	public int MaxStars;

	//Array of colors
	Color[] starColors = 
	{
		new Color(0.5f, 0.5f, 1f), //Blue
		new Color(0, 1f,1f), //Green
		new Color(1f,1f,0), //Yellow
		new Color(1f,0,0) //Red
	};

	// Use this for initialization
	void Start () 
	{
		//Bottom-Left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0,0));

		//Top-Right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

		//Loop, creating Stars
		for (int i = 0; i < MaxStars; ++i) 
		{
			GameObject star = Instantiate (starPrefab) as GameObject;

			//Set the Star Color
			star.GetComponent <SpriteRenderer>().color = starColors[i % starColors.Length];

			//Set the position of the star (random x and random y)
			star.transform.position = new Vector2 (Random.Range(min.x, max.x), Random.Range(min.y, max.y));

			//Set a random speed for the star
			star.GetComponent <Star>().speed = - (1f * Random.value + 0.5f);

			//Set the transform of Star with the StarGenerator's
			star.transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
