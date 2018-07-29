using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour 
{
	//GameManager, Player's Bullet Prefab, BulletPositions, ExplosionPrefab
	public GameObject GameManager;
	public GameObject playerBulletGo;
	public GameObject bulletPosition01;
	public GameObject bulletPosition02;
	public GameObject explosion;

	//Audiosource attached to Player
	AudioSource audioSource;

	//Reference to the lives UI Text
	public Text LivesUIText;

	//maximum player lives
	const int MaxLives = 3;
	//current player lives
	int lives;


	//what I tried
	//public Transform bulletPosition01;
	//public Transform bulletPosition02;

	//for the speed of Player
	public float speed;

	//Get the accelerometer Y Value at the Start of the game
	float accelStartY;

	public void Init()
	{
		lives = MaxLives;

		//Update the lives UI Text
		LivesUIText.text = lives.ToString ();

		//Reset the player posiiton to the center of the screen
		transform.position = new Vector2 (0,0); 

		//Set this player GameObject to active
		gameObject.SetActive (true);
	}

	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent <AudioSource> ();

		//get the initial accelerometer Y value
		accelStartY = Input.acceleration.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (Input.GetKeyDown (KeyCode.Space)) 
//		{
//			//play the laser sound effect
//			audioSource.Play ();
//
//			//instantiate bullets from position01
//			GameObject bullet01 = (GameObject)Instantiate (playerBulletGo);
//			//seeting the bullet's initial posiiton
//			bullet01.transform.position = bulletPosition01.transform.position;
//
//			//instantiate bullets from position02
//			GameObject bullet02 = (GameObject)Instantiate (playerBulletGo);
//			//seeting the bullet's initial posiiton
//			bullet02.transform.position = bulletPosition02.transform.position;
//
//			what I tried
//			Instantiate (playerBulletGo, bulletPosition01);
//			Instantiate (playerBulletGo, bulletPosition02);
//
//		}
			

//		// the value will be -1, 0, 1 (for left, no input, and right)
//		float x = Input.GetAxisRaw ("Horizontal"); 
//		// the value will be -1, 0, 1 (for down, no input, and up)
//		float y = Input.GetAxisRaw ("Vertical");
//
//		//based on the input, we compute a direction vector, and normalized it to get a unit vector
//		Vector2 direction = new Vector2 (x, y).normalized;

		//Get input from the accelerometer
		float x = Input.acceleration.x;
		float y = Input.acceleration.y - accelStartY;

		//Create a vector with the accelerometer input values
		Vector2 direction = new Vector2 (x, y);

		//clamp the length of the vector to a maximum of 1
		if (direction.sqrMagnitude > 1)
			direction.Normalize ();

		//Function to compute and set the player's position
		Move(direction);

	}
	void Move(Vector2 direction)
	{
		//Find the screen limits to the player's movement (left, right, top, and bottom of the screen)

		//the bottom-left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		//the top-right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		//substract the player sprite half width
		max.x = max.x - 0.225f;
		//add the player sprite half width
		min.x = min.x + 0.225f;

		//substract the player sprite half height
		max.y = max.y - 0.225f;
		//add the player sprite half height
		min.y = min.y + 0.225f;

		//Get the player's current position
		Vector2 currentPosition = transform.position;

		//Calculate the new position
		currentPosition += direction * speed* Time.deltaTime;
//		print ("direction : " + direction);
//		print ("currentPosition : " + currentPosition);

		//Make sure the new posiiton is not outside the screen
		currentPosition.x = Mathf.Clamp(currentPosition.x, min.x, max.x);
		currentPosition.y = Mathf.Clamp (currentPosition.y, min.y, max.x);

		//Update the player's position
		transform.position = currentPosition;
	}

	//Function to make the player shoot
	public void Shoot()
	{
		//play the laser sound effect
		audioSource.Play ();

		//instantiate bullets from position01
		GameObject bullet01 = (GameObject)Instantiate (playerBulletGo);
		//seeting the bullet's initial posiiton
		bullet01.transform.position = bulletPosition01.transform.position;

		//instantiate bullets from position02
		GameObject bullet02 = (GameObject)Instantiate (playerBulletGo);
		//seeting the bullet's initial posiiton
		bullet02.transform.position = bulletPosition02.transform.position;
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		//Detect collision of the player ship with an enemy ship, or with an enemy bullet
		if ((collider.tag == "EnemyTag" || (collider.tag == "EnemyBulletTag"))) 
		{
			PlayExplosion ();

			//Subtract one live
			lives--;

			//Update lives UI Text
			LivesUIText.text = lives.ToString ();

			//if Player is dead
			if (lives == 0) 
			{
				//Change Game Manager State to game over state
				GameManager.GetComponent<GameManager> ().SetGameManagerState (global::GameManager.GameManagerState.GameOver);


				//hide the player
				gameObject.SetActive (false);
			}
		}
	}

	//Function to instantiate an explosion
	void PlayExplosion()
	{
		GameObject explosion = Instantiate (this.explosion) as GameObject;

		//Set the posion of the explosion
		explosion.transform.position = transform.position;
	}
}
