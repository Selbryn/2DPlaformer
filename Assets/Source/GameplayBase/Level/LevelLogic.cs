using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour {

	[Space (10.0f)]
	public 	float	levelElapsedTime;	//Tiempo que ha transcurrido mientras el nivel esta abierto
	private float	startingTime;		//Momento en el que comienza el nivel

	[Space (10.0f)]
	public 	int		currentNumberRoom;		//La habitacion actual activa
	[Range (0,5)]
	public 	int 	maxLevelIterations;		//El numero máximo de iteraciones que tiene este nivel
	public 	int 	currentLevelIteration;	//El numero de iteracion del nivel


	[Space (10.0f)]
	[Range (1, 2)]
	public 	int 			numOfPlayers;	//Numero de personajes a sacar
	public 	GameObject 		playerPrefab;	//El prefab del personaje
	public	GameObject[]	playersSpawned;	//Los jugadores spawneados

	private LevelCreator levelCreator;	//El componente encargado de crear el nivel
	private Camera		 mainCamera;	//La camara principal

	private Notification OnFinishLevel;	//Notificacion de terminar el nivel

#region Public Methods 

	public void Awake(){

		levelCreator = GetComponent<LevelCreator> ();
		mainCamera = Camera.main;

		currentNumberRoom = 0;
		currentLevelIteration = 0;

		OnFinishLevel = new Notification (NotificationTypes.onlevelfinished);
		NotificationCenter.defaultCenter.addListener (OnRoomFinished, NotificationTypes.onroomfinished);
	}

	public IEnumerator Start () {

		levelCreator.BuildRooms ();

		while(!levelCreator.IsLevelCreated){

			yield return new WaitForEndOfFrame();
		}

		EnableRoom ();
		RestartLevel ();
		SpawnPlayers ();
	}

#endregion

#region Private Methods 

	private void RestartLevel(){

		startingTime = Time.time;
		levelElapsedTime = 0.0f;
	}
		
	private void SpawnPlayers(){

		System.Array.Resize (ref playersSpawned, numOfPlayers);
		Vector3	nextSpawnPointPos = levelCreator.levelRooms[0].SpawnPoint.position;

		for(int i =0; i < numOfPlayers; i++){

			playersSpawned[i] = Instantiate (playerPrefab, nextSpawnPointPos, Quaternion.identity);
		}
	}

	/// <summary>
	/// Activamos una habitacion en concreto
	/// </summary>
	private void EnableRoom(){

		levelCreator.levelRooms [currentNumberRoom].EnableRoomIteration (currentLevelIteration);
	}

	/// <summary>
	/// Desactivamos una habitacion en concreto
	/// </summary>
	private void DisableRoom(){

		levelCreator.levelRooms [currentNumberRoom].DisableRoomIteration (currentLevelIteration);
	}

	/// <summary>
	/// Movemos al pj hasta la siguiente habitacion
	/// </summary>
	private void MoveCharacterToNextRoom(){

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
	/// Se calcula el numero de la siguiente habitacion a activar.
	/// Se sigue el orden segun esten instanciadas hasta que llega a la ultima
	/// al llegar a la ultima se vuelve a colocar en la primera a no se que haya hecho el maximo de iteraciones por el nivel.
	/// </summary>
	private void CalculateRoomNumber(){

		//Si no hemos llegado al final
		if (currentNumberRoom < levelCreator.levelRooms.Length - 1) {

			currentNumberRoom++;
		//Si hemos llegado al final volvemos a la primera y sumamos uno a las iteraciones
		} else {

			if (currentLevelIteration == maxLevelIterations) {

				NotificationCenter.defaultCenter.postNotification (OnFinishLevel);
				Debug.Break ();
			} else {

				currentNumberRoom = 0;
				currentLevelIteration++;
			}
		}
	}

	/// <summary>
	/// Calculamos el tiempo invertido en el nivel.
	/// </summary>
	private void CalculateLevelTime(){

		levelElapsedTime = (Time.time - startingTime);
	}

#endregion

#region Notifications

	/// <summary>
	/// Evento de terminar una room
	/// Aqui dentro se desencadena el cambio de habitacion
	/// </summary>
	public void OnRoomFinished(Notification note){

		DisableRoom ();
		CalculateRoomNumber ();
		EnableRoom ();
		MoveCharacterToNextRoom ();
		PlaceCamera ();
		CalculateLevelTime ();
	}

#endregion

}
