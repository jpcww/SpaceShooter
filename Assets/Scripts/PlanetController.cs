using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetController : MonoBehaviour 
{
	//Array of planet prefabs
	public GameObject[] Planets;

	//Queue to hold the planets
	Queue<GameObject> availablePlanets = new Queue<GameObject>();


	// Use this for initialization
	void Start () 
	{
		//Add the planets to the Queue (Enqueue them)
		availablePlanets.Enqueue (Planets[0]);
		availablePlanets.Enqueue (Planets[1]);
		availablePlanets.Enqueue (Planets[2]);

		//Call the MovePlanetDown Function every 20 seconds
		InvokeRepeating ("MovePlanetDown", 0, 20f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Funtion to dequeue a planet, and set its moving flag to true
	//so taht the plnaet starts scrolling down the screen

	void MovePlanetDown()
	{
		EnqueuePlanets ();

		//if the Queue is empty, then return
		if (availablePlanets.Count == 0)
			return;

		//get a planet from the queue
		GameObject aPlanet = availablePlanets.Dequeue ();

		//set the planet moving flag to true
		aPlanet.GetComponent <Planet>().moving = true;
	}

	void EnqueuePlanets()
	{
		foreach (GameObject aPlanet in Planets) 
		{
			//if the planet is below the screen and the planet is not moving
			if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent <Planet> ().moving)) 
			{
				//reset the planet position
				aPlanet.GetComponent <Planet>().ResetPosition ();

				//Enqueue the planet
				availablePlanets.Enqueue (aPlanet);
			}
		}
	}
}
