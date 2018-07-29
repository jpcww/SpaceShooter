using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour 
{
	//for the bullet speed
	float speed;
	//for the direction of the bullet
	Vector2 direction;
	//to know when the bullet is set
	bool ready;

	//set the default values before the game starts
	void Awake()
	{
		speed = 5f;
		ready = false;
	}


	// Use this for initialization
	void Start () {
		
	}

	//Fuction to set the bullet's direction 
	public void SetDirection (Vector2 direction)
	{
		//set the direction normalized, to get an unit vector
		this.direction = direction.normalized;

		//set the flag to true
		ready = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ready)
		{
			//get the bullet's position property
			Vector2 position = transform.position;

			//compute the bullet's new position
			position += this.direction * speed * Time.deltaTime;

			//update the bullet's position
			transform.position = position;


			//remove the bullet from our game when it goes out of the screen
			//bottom-left point of the screen
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
			//top-right point of the screen
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1,1));

			//if the bullet goes out of the screen, destroy the bullet
			if((transform.position.x < min.x) || (transform.position.x > max.x) 
				|| (transform.position.y < min.y) || (transform.position.y > max.y))
			{
				Destroy (gameObject);
			}
		}
	}


	void OntriggerEnter2D(Collider2D collider)
	{
		//Detect collision of an enemy's bullet with player
		if (collider.tag == "PlayerTag")
		{
			//Destroy this enemy's bullet
			Destroy (gameObject);
		}
	}
}
