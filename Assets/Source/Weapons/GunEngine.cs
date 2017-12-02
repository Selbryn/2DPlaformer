using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEngine : MonoBehaviour {

	public float 	timeToRecharge;			//Tiempo para recargar una carga
	public int 		maxNumOfCharges;		//Numero de cargas maximo que puede soportar el arma
	public bool		isFullCharged;
		
	public 	float	currentTime;
	private float	lastStageTime;			//Momento en el que se consiguio el ultimo stage
	public 	int 	currentNumOfCharges;

			

	void Start () {

		currentNumOfCharges = 0;
	}
	
	void Update () {

		//Si no tenemos todos las cargas acumuladas seguimos contando el tiempo
		if (currentNumOfCharges < maxNumOfCharges) {

			currentTime = Time.time - lastStageTime; 

		//Si tenemos todas las cargas
		} else {
			
			currentTime = 0.0f;
			lastStageTime = Time.time;
			isFullCharged = true;
		}

		//Si hemos llegado al tiempo para adquirir una carga la cargamos
		if(currentTime >= timeToRecharge){

			//Aumentamos en 1 la carga
			currentNumOfCharges++;
			//Reseteamos el timer
			lastStageTime = Time.time;
			currentTime = 0.0f;
		}

		if(Input.GetMouseButtonDown(0)){

			Debug.Log("...");
			UseCharge (1);
		}
	}

	protected void UseCharge(int numOfCharges){

		if(currentNumOfCharges == 0){

			return;
		}

		if(currentNumOfCharges >= numOfCharges){

			currentNumOfCharges -= numOfCharges;
			isFullCharged = false;
			//Debug.Break ();
		}

	}

}
