using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;

	private Notification OnPlayerDie;

	void Awake () {
	
		NotificationCenter.defaultCenter.addListener (OnReceiveDamage, NotificationTypes.onrecievedamage);
		OnPlayerDie = new Notification (NotificationTypes.onplayerdie);	

		currentHealth = maxHealth;
	}

	private void OnReceiveDamage(Notification note){

		int damageTaken = (note as IntNotification).varInt;

		if (damageTaken >= currentHealth) {

			currentHealth = 0;
			NotificationCenter.defaultCenter.postNotification (OnPlayerDie);
		} else {

			currentHealth = currentHealth - damageTaken;
		}
	}
}
