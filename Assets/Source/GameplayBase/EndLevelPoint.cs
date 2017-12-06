using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPoint : MonoBehaviour {

	public Notification OnLevelFinished;	//Notificacion de finalizar nivel

	void Awake(){

		OnLevelFinished = new Notification (NotificationTypes.onlevelfinished);
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag.ToString() == "Player"){

			NotificationCenter.defaultCenter.postNotification (OnLevelFinished);
		}
	}

}
