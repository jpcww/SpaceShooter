 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour 
{
	//Speed for Star
	public float speed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get the position of Star, this script is attached to
		Vector2 position = transform.position;

		//Change the position of Star
		position = new Vector2(position.x, position.y + speed*Time.deltaTime);

		//Apply the change to Transform.position
		transform.position = position;

		//Bottom-left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0,0));
		//Top-Right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		if (transform.position.y < min.y) 
		{
			transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		}
	}
}
