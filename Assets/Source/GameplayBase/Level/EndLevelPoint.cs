using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPoint : MonoBehaviour {

	public Notification OnRoomFinished;	//Notificacion de finalizar nivel

	void Awake(){

		OnRoomFinished = new Notification (NotificationTypes.onroomfinished);
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag.ToString() == "Player"){

			NotificationCenter.defaultCenter.postNotification (OnRoomFinished);
		}
	}
}
