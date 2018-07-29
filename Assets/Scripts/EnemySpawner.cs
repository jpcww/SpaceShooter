  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	//To use "Enemy Prefab" in the script as a component
	public GameObject enemy;

	//for the frequency of spawning enemies
	float maxSpawnRateInSeconds = 5f;

	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Custom Function to spawn an enemy
	void SpawnEnemy()
	{
		//bottom-left of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		//top-right of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		//Instantitate an enemy
		GameObject anEnemy = (GameObject)Instantiate (enemy);
		//Set the position of enemy random
		anEnemy.transform.position = new Vector2 (Random.Range(min.x, max.x), max.y);

		//Schedule when to spwan next enemy
		ScheduleNextEnemySpawn();
	}

	//Custom Fuction
	void ScheduleNextEnemySpawn()
	{
		float spawnInSeconds;

		if (maxSpawnRateInSeconds > 1f) 
		{
			//Pick a number between 1 and maxSpawnRateInseconds
			spawnInSeconds = Random.Range (1f, maxSpawnRateInSeconds);
		} 

		else
			spawnInSeconds = 1f;

		Invoke ("SpawnEnemy", spawnInSeconds);
	}

	//Custom function to increase the difficulty of the game
	void IncreaseSpawnRate()
	{
		if (maxSpawnRateInSeconds > 1f)
		{
			maxSpawnRateInSeconds--;
		}

		if (maxSpawnRateInSeconds == 1f) 
		{
			CancelInvoke ("IncreaseSpawnRate");
		}
	}

	//Function to Start enemy spawner
	public void ScheduleEnemySpawner()
	{
		//Reset Max Spawn Rate
		maxSpawnRateInSeconds = 5f;

		Invoke ("SpawnEnemy", maxSpawnRateInSeconds);

		//Increase Spawn Rate every 30 seconds
		InvokeRepeating ("IncreaseSpawnRate", 0f, 30f);
	}


	//Function to sotp enemy spawner
	public void UnscheduleEnemySpawner()
	{
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("IncreaseSpwanRate");
	}

}
