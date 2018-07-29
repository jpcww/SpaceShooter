using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour 
{
	//reference to TextUI
	GameObject scoreUIText;

	//explosion prefab
	public GameObject explosion;

	//for the speed of enemy
	float speed;

	// Use this for initialization
	void Start () 
	{
		//set the speed in the first frame of the game
		speed = 2f;
		//get ScoreTextUI with tag
		scoreUIText = GameObject.FindGameObjectWithTag ("ScoreTextUI");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get the enemy's current position
		Vector2 position = transform.position;

		//Compute the enemy's new position
		position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

		//Update the enemy's position
		transform.position = position;

		//The bottom-left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

		//if the enemy gose outsie of the screen on the bottome, Destroy the enemy
		if (transform.position.y < min.y) 
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		//Detect collision of the enemy ship with the player ship, or with a player's bullet
		if (collider.tag == "PlayerTag" || (collider.tag == "PlayerBulletTag"))
		{
			PlayExplosion ();

			//add 100 point to the score
			scoreUIText.GetComponent <GameScore>().Score += 100;

			//Destroy this enemy ship
			Destroy (gameObject);
		}
	}

	//function to instantiate an explosion
	void PlayExplosion()
	{
		GameObject explosion = Instantiate (this.explosion) as GameObject;

		//Set the position of the explosion
		explosion.transform.position = transform.position;
	}
}
