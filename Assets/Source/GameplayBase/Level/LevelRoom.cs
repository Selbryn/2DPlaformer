using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoom : MonoBehaviour {

	public float 	roomWidth;		//Ancho de la habitacion

	private int roomIteration;		//Iteracion de la habitacion	
	private Transform spawnPoint;	//Referencia al spawnpoint de esta habitacion

	void Awake () {
	
		FindSpawnPoint ();
	}

	private void FindSpawnPoint(){

		//Buscamos el spawn point y lo cacheamos
		spawnPoint = transform.FindChild("PlayerSpawnPoint");
		if(spawnPoint == null){

			Debug.LogWarning ("SpawnPoint no encontrado en: " + transform.name);
		}
	}

#region Getter/Setter

	public Transform SpawnPoint{

		get{
			return spawnPoint;
		}
	}

	public int RoomIteration{

		set{
			roomIteration = value;
		}
	}

#endregion

}
