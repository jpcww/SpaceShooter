﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour 
{
	//for the speed of bullet
	public float speed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get the bullet's current position
		Vector2 position = transform.position; 

		//compute the bullet's new position
		position = new Vector2(position.x, position.y + speed*Time.deltaTime);

		//update the bullet's position
		transform.position = position;

		//this is the top-right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		//if the bullet goes outside the screen on the top
		if (transform.position.y > max.y) 
		{
			Destroy (gameObject);
		}
	}


 	void OntriggerEnter2D(Collider2D collider)
	{
		//Detect collsion of the player bullet with an enmey ship
		if (collider.tag == "EnemyTag") 
		{
			//Destroy this player bullet
			Destroy (gameObject);
		}
	}
}
