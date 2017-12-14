using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour {

	[Space (10.0f)]
	public 	float	elapsedTime;	//Tiempo que ha transcurrido mientras el nivel esta abierto
	private float	startingTime;	//Momento en el que comienza el nivel
	private	bool	isTimeRunning;	//Esta el tiempo corriendo?

	[Space (10.0f)]
	public 	int		currentNumberRoom;

	[Space (10.0f)]
	[Range (1, 2)]
	public 	int 			numOfPlayers;	//Numero de personajes a sacar
	public 	GameObject 		playerPrefab;	//El prefab del personaje
	public	GameObject[]	playersSpawned;	//Los jugadores spawneados

	private LevelCreator levelCreator;	//El componente encargado de crear el nivel
	private Camera		 mainCamera;	//La camara principal

	public void Awake(){

		levelCreator = GetComponent<LevelCreator> ();
		mainCamera = Camera.main;
		currentNumberRoom = 0;
	}

	public IEnumerator Start () {

		NotificationCenter.defaultCenter.addListener (OnRoomFinished, NotificationTypes.onroomfinished);

		while(!levelCreator.IsLevelCreated){

			yield return new WaitForEndOfFrame();
		}

		EnableRoom (currentNumberRoom);
		RestartLevel ();
		SpawnPlayers ();
	}
	
	public void Update () {

		if(isTimeRunning){

			elapsedTime = (Time.time - startingTime);
		}
	}

	public void RestartLevel(){

		isTimeRunning = true;
		startingTime = Time.time;
	}
		
	private void SpawnPlayers(){

		System.Array.Resize (ref playersSpawned, numOfPlayers);
		Vector3	nextSpawnPointPos = levelCreator.levelRooms[0].SpawnPoint.position;

		for(int i =0; i < numOfPlayers; i++){

			playersSpawned[i] = Instantiate (playerPrefab, nextSpawnPointPos, Quaternion.identity);
		}
	}

	private void EnableRoom(int roomToEnable){

		levelCreator.levelRooms[roomToEnable].gameObject.SetActive (true);
	}

	private void DisableRoom(int roomToDisable){

		levelCreator.levelRooms[roomToDisable].gameObject.SetActive (false);
	}

	private void MoveCharacterToNextRoom(){

		DisableRoom (currentNumberRoom - 1);
		EnableRoom (currentNumberRoom);
		playersSpawned [0].transform.position = levelCreator.levelRooms [currentNumberRoom].SpawnPoint.transform.position;
	}

	/// <summary>
	/// Coloca la camara enfocando al a habitacion actual
	/// </summary>
	private void PlaceCamera(){

		Vector3	currentRoomPos = levelCreator.levelRooms [currentNumberRoom].transform.position;
		Vector3 newCameraPos = new Vector3 (currentRoomPos.x, currentRoomPos.y, -10); 
		mainCamera.transform.position = newCameraPos;
	}

	/// <summary>
	/// Evento de terminar una room
	/// </summary>
	public void OnRoomFinished(Notification note){

		isTimeRunning = false;
		currentNumberRoom++;
		MoveCharacterToNextRoom ();
		PlaceCamera ();
	}
}
