using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour 
{
	//Speed of Planet
	public float speed;
	//Flag to make the planet scroll down the screen
	public bool moving;

	//bottom-left point of the screen
	Vector2 min;
	//top-right point of the screen
	Vector2 max;

	void Awake()
	{
		moving = false;

		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		max = Camera.main.ViewportToWorldPoint (new Vector2(1,1));

		//Add the planet spirte's half height to max y
		max.y = max.y + GetComponent <SpriteRenderer>().sprite.bounds.extents.y;
		//SubTranct the planet sprite's half height to min y
		min.y = min.y - GetComponent <SpriteRenderer>().sprite.bounds.extents.y;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!moving)
			return;

		//Get the transform.position of Star
		Vector2 position = transform.position;

		//Change the transform.position of Star
		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

		//Apply the change
		transform.position = position;

		//if planet gets to the minimum y position, stop moving planet
		if (transform.position.y < min.y) 
		{
			moving = false;
		}
	}

	//Funtion  to reset the planet's position
	public void ResetPosition()
	{
		// reset the position of the planet to random x, random y
		transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
	}
}
