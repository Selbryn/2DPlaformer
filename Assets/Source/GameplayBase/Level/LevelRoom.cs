using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoom : MonoBehaviour {

	public float 			roomWidth;			//Ancho de la habitacion

	public List<Transform>	roomIterations;		//Referencia a las diferentes iteraciones de la habitacion
	private Transform 		spawnPoint;			//Referencia al spawnpoint de esta habitacion

	void Awake () {
	
		FindSpawnPoint ();
		FindAllRoomIterations ();
	}

#region Public Methods

	/// <summary>
	/// Activamos la habitacion y su iteracion
	/// </summary>
	public void EnableRoomIteration(int roomIteration){

		gameObject.SetActive (true);
		roomIterations [roomIteration].gameObject.SetActive (true);
	}

	/// <summary>
	/// Desactivamos la habitacion y la iteracion
	/// </summary>
	public void DisableRoomIteration(int roomIteration){

		gameObject.SetActive (false);
		roomIterations [roomIteration].gameObject.SetActive (false);
	}

#endregion

#region Private Methods

	/// <summary>
	/// Buscamos el spanw point y lo cacheamos
	/// </summary>
	private void FindSpawnPoint(){

		spawnPoint = transform.FindChild("PlayerSpawnPoint");
		if(spawnPoint == null){

			Debug.LogWarning ("SpawnPoint no encontrado en: " + transform.name);
		}
	}

	/// <summary>
	/// Buscamos todas las iteraciones de la habitacion y las cacheamos
	/// </summary>
	private void FindAllRoomIterations(){

		for(int i = 0; i < transform.childCount; i++){

			if(transform.GetChild(i).name.Contains("IT_")){

				roomIterations.Add (transform.GetChild(i));
			}
		}
	}

#endregion

#region Getter/Setter

	public Transform SpawnPoint{

		get{
			return spawnPoint;
		}
	}
		
#endregion

}
