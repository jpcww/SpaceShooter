using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour 
{
	//Reference to playButton, Player, GameManager, GameOver, TextUI ,TimerCounter ,GameTitle and ShootButton
	public GameObject playButton;
	public GameObject playerShip;
	public GameObject enemySpawner;
	public GameObject gameOver;
	public GameObject scoreTextUI;
	public GameObject timeCounter;
	public GameObject gameTitle;
	public GameObject shootButton;

	public enum GameManagerState
	{  
		Opening,
		GamePlay,
		GameOver
	}

	GameManagerState GMState;

	// Use this for initialization
	void Start () 
	{
		GMState = GameManagerState.Opening;
	}

	//Function to update GameManager state
	void UpdateGameManagerState()
	{
		switch (GMState)
		{
			case GameManagerState.Opening:
				
				//Hide Game Over
				gameOver.SetActive (false);

				//Display the Game Title
				gameTitle.SetActive (true);

				//Set PlayButton Visible Acitve
				playButton.SetActive (true);
				break;

			case GameManagerState.GamePlay:

				//Reset the score to 0
				scoreTextUI.GetComponent <GameScore>().Score = 0;

				//Hide Play Button on Game Play State
				playButton.SetActive (false);

				//Gide the Game Title
				gameTitle.SetActive (false);

				//DisPlay the shoot button
				shootButton.SetActive (true);

				//Set Player visible Active and Initialize Init Function to start Play lives
				playerShip.GetComponent <PlayerControl>().Init();

				//Start Enemy Spawner
				enemySpawner.GetComponent <EnemySpawner>().ScheduleEnemySpawner ();
				

				//Start Time Counter
				timeCounter.GetComponent <TimerCounter>().StartTimeCounter();
				break;


			case GameManagerState.GameOver:

				//Stop the Time counter
				timeCounter.GetComponent <TimerCounter>().StopTimeCounter ();

				//Stop enemy spawner
				enemySpawner.GetComponent<EnemySpawner> ().UnscheduleEnemySpawner ();

				//Hide Shoot Button
				shootButton.SetActive (false);

				//Display game over
				gameOver.SetActive (true);

				//Change Game Manager State to opening state
				Invoke ("ChangeToOpeningState", 8f);

				break;
		}
	}

	//function to set GameManager State
	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}

	//Function to Start Game when Player clicks PlayButton
	public void StartGamePlay()
	{
		GMState = GameManagerState.GamePlay;
		UpdateGameManagerState ();
	}

	//Function to change game manager state to opening state
	public void ChangeToOpeningState()
	{
		SetGameManagerState (GameManagerState.Opening);
	}
}
