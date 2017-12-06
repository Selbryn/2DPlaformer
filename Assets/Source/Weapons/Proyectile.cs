using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour {

	public float	timeToDestroy;

	void Start () {

		Destroy (this.gameObject, timeToDestroy);
	}

	void OnCollisionEnter2D(Collision2D other){

		//Destroy (this.gameObject);
	}
}
