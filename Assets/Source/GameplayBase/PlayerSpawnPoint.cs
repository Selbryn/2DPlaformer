using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour {

	public GameObject SpawnPlayer(GameObject player) {

		return Instantiate (player, transform.position, Quaternion.identity);
	}

}
