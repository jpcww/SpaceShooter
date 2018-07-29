using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour 
{
	//Reference to the timer counter UI Text
	Text timeUI;

	float startTime;//the time when the suer click on Play
	float ellapsedTime;//The ellapsed time after the user clicks on play
	bool startCounter;//Flag to star the counter

	int minutes;
	int seconds;

	// Use this for initialization
	void Start () 
	{
		//Set the flag to false
		startCounter = false;

		//Get the Text component
		timeUI = GetComponent <Text>();
	}


	//Function to start timer Counter
	public void StartTimeCounter()
	{
		startTime = Time.time;
		startCounter = true;
	}

	//Funtion to stop the time counter
	public void StopTimeCounter()
	{
		startCounter = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (startCounter) 
		{
			//Compute the ellapsed time
			ellapsedTime = Time.time - startTime;

			//Get the minutes
			minutes = (int)ellapsedTime / 60;
			//Get the Seconds
			seconds =(int)ellapsedTime % 60;

			//Update the Time Counter UI Text
			timeUI.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
		}
	}
}
