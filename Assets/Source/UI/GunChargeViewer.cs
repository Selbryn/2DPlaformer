using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChargeViewer : MonoBehaviour {

	public Image[] 			chargesSprites;	//Los sprites de la UI que representan las cargas del arma

	private BasicGun		currentGun;		//El arma actual

	public void Awake () {

		NotificationCenter.defaultCenter.addListener (OnPlayerSpawned, NotificationTypes.onplayerspawned);
	}

	void Update () {

		if(currentGun != null){

			SetSprites (currentGun.currentNumOfCharges);
		}
	}

	private void SetSprites(int currentNumOfSprites){

		for (int i = 0; i < chargesSprites.Length; i++){

			if (i < currentNumOfSprites) {

				chargesSprites [i].enabled = true;
			} else {

				chargesSprites [i].enabled = false;
			}
		}
	}

	private void OnPlayerSpawned(Notification note){

		currentGun = FindObjectOfType<BasicGun> ();
	}
}
