using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour 
{
	//to use EnemyBullet Prefab in this script as a component
	public GameObject EnemyBullet;

	// Use this for initialization
	void Start () 
	{
		//fire an enemy bullet after 1 second
		Invoke ("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Function to fire an enemy's bullet
	void FireEnemyBullet()
	{
		//get a reference of the player
		GameObject player = GameObject.Find("Player");

		//if player is not dead yet
		if (player != null) 
		{
			//instantiate an enemy bullet
			GameObject bullet = Instantiate (EnemyBullet) as GameObject;

			//set the bullet's initial position same as the EnemyGun's position
			bullet.transform.position = transform.position;

			//compute the bullet's direction towards the player's ship
			Vector2 direction = player.transform.position - bullet.transform.position;

			//set the bullet's direction
			bullet.GetComponent <EnemyBullet>().SetDirection (direction);
		}
	}


}
