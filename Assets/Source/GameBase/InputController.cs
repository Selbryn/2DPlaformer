using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input controller.
/// </summary>
public class InputController : MonoBehaviour {

	public Vector3Notification 	OnInputDone; 			//Notificacion de input de movimiento recibido
	public Notification 		OnInputFinished; 		//Notificacion de input de terminado
	public Notification 		OnJumpPressed;			//Notificacion de salto
	public Notification 		OnShotPressed;			//Notificacion de disparo

	[Header ("Player One")]
	public KeyCode 				moveRightKey;			//Tecla para moverse a la derecha
	public KeyCode 				moveLeftKey;			//Tecla para moverse a la izquierda
	public KeyCode 				jumpKey;				//Tecla para saltar
	public KeyCode				shotKey;

	private bool 				isRightArrowPressed;	//Esta apretada la tecla derecha?
	private bool 				isLeftArrowPressed;		//Esta apretada la tecla izquierda?

	void Awake () {

		OnInputDone 	= new Vector3Notification (NotificationTypes.oninputdone, Vector3.zero);
		OnInputFinished = new Notification (NotificationTypes.oninputfinished);
		OnJumpPressed 	= new Notification (NotificationTypes.onjumppressed);
		OnShotPressed 	= new Notification (NotificationTypes.onshotpressed);
	}
	

	void Update () {

		//Apretamos la tecla izquierda
		if( (Input.GetKeyDown(moveLeftKey))) {

			isLeftArrowPressed = true;

			if(!isRightArrowPressed){

				OnInputDone.varVector3 = Vector3.left;
				NotificationCenter.defaultCenter.postNotification (OnInputDone);
			}
		}

		//Apretamos la tecla derecha
		if( (Input.GetKeyDown(moveRightKey))){

			isRightArrowPressed = true;

			if(!isLeftArrowPressed){

				OnInputDone.varVector3 = Vector3.right;
				NotificationCenter.defaultCenter.postNotification (OnInputDone);
			}
		}

		//Apretamos tecla del salto
		if( Input.GetKeyDown(jumpKey) ){
		
			NotificationCenter.defaultCenter.postNotification (OnJumpPressed);
		}

		//Soltamos alguna tecla de movimiento
		if( (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && isLeftArrowPressed){

			NotificationCenter.defaultCenter.postNotification (OnInputFinished);
			isLeftArrowPressed = false;

			if(isRightArrowPressed){

				OnInputDone.varVector3 = Vector3.right;
				NotificationCenter.defaultCenter.postNotification (OnInputDone);
			}
		}

		if( (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && isRightArrowPressed){

			NotificationCenter.defaultCenter.postNotification (OnInputFinished);
			isRightArrowPressed = false;

			if(isLeftArrowPressed){

				OnInputDone.varVector3 = Vector3.left;
				NotificationCenter.defaultCenter.postNotification (OnInputDone);
			}
		}

		//Apretamos el boton de disparo
		if((Input.GetKeyDown(shotKey))){

			NotificationCenter.defaultCenter.postNotification (OnShotPressed);
		}

	}
}
