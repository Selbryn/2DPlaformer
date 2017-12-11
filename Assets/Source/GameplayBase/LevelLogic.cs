using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour {

	[Space (10.0f)]
	public 	float	elapsedTime;	//Tiempo que ha transcurrido mientras el nivel esta abierto
	 
	private float	startingTime;	//Momento en el que comienza el nivel
	private	bool	isTimeRunning;	//Esta el tiempo corriendo?

	[Space (10.0f)]
	[Range (1, 2)]
	public 	int 				numOfPlayers;	//Numero de personajes a sacar
	public 	GameObject 			playerPrefab;	//El prefab del personaje
	public	GameObject[]		playersSpawned;	//Los jugadores spawneados
	private PlayerSpawnPoint	spawnPoint;		//Referencia al spawnpoint

	public void Awake () {

		spawnPoint = GameObject.FindObjectOfType<PlayerSpawnPoint> ();

		NotificationCenter.defaultCenter.addListener (OnLevelFinished, NotificationTypes.onlevelfinished);
		RestartLevel ();
	}
	
	public void Update () {

		if(isTimeRunning){

			elapsedTime = (Time.time - startingTime);
		}
	}

	public void OnLevelFinished(Notification note){

		isTimeRunning = false;
	}

	public void RestartLevel(){

		isTimeRunning = true;
		startingTime = Time.time;

		SpawnPlayers ();
	}

	private void SpawnPlayers(){

		System.Array.Resize (ref playersSpawned, numOfPlayers);

		for(int i =0; i < numOfPlayers; i++){

			playersSpawned[i] = spawnPoint.SpawnPlayer (playerPrefab);
		}
	}
}
