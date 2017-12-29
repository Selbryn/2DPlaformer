using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPoint : MonoBehaviour {

	public Notification OnRoomFinished;	//Notificacion de finalizar la room

	void Awake(){

		OnRoomFinished = new Notification (NotificationTypes.onroomfinished);
	}

	/// <summary>
	/// En el momento en el que el jugador toca este trigger se manda el evento de room completada 
	/// </summary>
	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag.ToString() == "Player"){

			NotificationCenter.defaultCenter.postNotification (OnRoomFinished);
		}
	}
}
