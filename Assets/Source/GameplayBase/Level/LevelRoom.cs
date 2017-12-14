using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoom : MonoBehaviour {

	public float roomWidth;

	private Transform spawnPoint;	//Referencia al spawnpoint de esta habitacion

	void Awake () {
	
		//Buscamos el spawn point y lo cacheamos
		spawnPoint = transform.FindChild("PlayerSpawnPoint");
		if(spawnPoint == null){

			Debug.LogWarning ("SpawnPoint no encontrado en: " + transform.name);
		}
	}

	public Transform SpawnPoint{

		get{
			return spawnPoint;
		}
	}
}
