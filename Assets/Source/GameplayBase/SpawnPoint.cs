using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public int 			numOfCharacters;
	public GameObject 	character;

	void Start() {

		SpawnPlayer (true);
	}

	private void SpawnPlayer(bool isFirstTime) {

		for(int i = 0; i < numOfCharacters; i++){

			Instantiate (character, transform.position, Quaternion.identity);
		}

	}

}
