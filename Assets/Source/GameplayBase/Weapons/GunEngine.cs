using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AimingSystem))]
public class GunEngine : MonoBehaviour {

	[Space (10.0f)]
	public		Transform	proyectilePrefab;

	[Space (10.0f)]
	public 		float	timeToRecharge;			//Tiempo para recargar una carga
	public 		int 	maxNumOfCharges;		//Numero de cargas maximo que puede soportar el arma
	public 		bool	isFullCharged;

	[Space (10.0f)]
	public		int 	currentNumOfCharges;	//El numero actual de cargas
	protected	float	currentTime;			//El tiempo actual que ha pasado despues de la ultima recarga
	protected 	float	lastStageTime;			//Momento en el que se consiguio el ultimo stage
	protected	AimingSystem	aimingSystem;

	protected virtual void Awake(){

		aimingSystem = GetComponent<AimingSystem> ();
	}

	protected virtual void Start () {

		currentNumOfCharges = 0;
		NotificationCenter.defaultCenter.addListener (OnShotPressed, NotificationTypes.onshotpressed);
	}
	
	protected virtual void Update () {

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
	}

	protected virtual bool CanUseCharge(int numOfCharges){

		//Si el numero de cargas actuales es 0
		if(currentNumOfCharges == 0){

			return false;
		}

		//No gasta cargas
		if(numOfCharges == 0){

			Debug.LogWarning ("Revisar cargas de este arma");
			return false;
		}

		//Si tenemos más cargas de las que se gastan
		if (currentNumOfCharges >= numOfCharges) {
		
			currentNumOfCharges -= numOfCharges;
			isFullCharged = false;
			return true;
		} else {

			return false;
		}
	}

	protected virtual void OnShotPressed(Notification note){ }
}
