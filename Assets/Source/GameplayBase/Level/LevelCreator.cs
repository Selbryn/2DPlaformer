using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se encarga de crear el nivel, colocar las rooms y demas...
/// </summary>
public class LevelCreator : MonoBehaviour {

	[Space (10.0f)]
	[Range (1, 6)]
	public int 			numOfRooms;			//El numero total de rooms de este nivel
	public LevelRoom 	availableRooms;		//Las rooms disponibles para este nivel
	public LevelRoom[]	levelRooms;			//Convertir en un array mas adelante

	private bool 		isLevelCreated;		//Se ha creado el nivel?

	void Awake () {

		isLevelCreated = false;
	}

	/// <summary>
	/// Contruimos las habitaciones
	/// </summary>
	public void BuildRooms(){

		//Resizeamos el array
		System.Array.Resize (ref levelRooms, numOfRooms);
		//Posicion inicial de la primera habitacion
		Vector3 nextRoomPosition = Vector3.zero;

#if DEBUG
		Debug.Log("Init build time:" + Time.time);
#endif

		//Creamos tantas habitaciones como este indicado
		for(int i = 0; i < numOfRooms; i++){

			nextRoomPosition = new Vector3 (nextRoomPosition.x + (i * availableRooms.roomWidth), nextRoomPosition.y, nextRoomPosition.z);
			levelRooms[i] = Instantiate<LevelRoom> (availableRooms, nextRoomPosition, Quaternion.identity);
			levelRooms[i].name = "Room_" + i;
		}

		isLevelCreated = true;

#if DEBUG 
		Debug.Log("End build time:" + Time.time);
#endif
	}

	/// <summary>
	/// Te dice si el nivel se ha creado o no
	/// </summary>
	public bool IsLevelCreated{

		get{
			return isLevelCreated;
		}
	}
}
