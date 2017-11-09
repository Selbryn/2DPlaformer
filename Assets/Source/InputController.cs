using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public Vector3Notification 	OnInputDone; 			//Notificacion de input de movimiento recibido
	public Notification 		OnInputFinished; 		//Notificacion de input de terminado
	public Notification 		OnJumpPressed;			//Notificacion de salto
	public Notification 		OnReturnToGround;		//Notificacion de volver al suelo

	public KeyCode 				jumpKey;
	public KeyCode 				returnToGround;

	private bool 				isRightArrowPressed;
	private bool 				isLeftArrowPressed;

	void Awake () {


		OnInputDone 	= new Vector3Notification (NotificationTypes.oninputdone, Vector3.zero);
		OnInputFinished = new Notification (NotificationTypes.oninputfinished);
		OnJumpPressed 	= new Notification (NotificationTypes.onjumppressed);
		OnReturnToGround = new Notification (NotificationTypes.onreturntoground);
	}
	

	void Update () {

		//Apretamos la tecla izquierda
		if( (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isRightArrowPressed) {

			OnInputDone.varVector3 = Vector3.left;
			NotificationCenter.defaultCenter.postNotification (OnInputDone);
			isLeftArrowPressed = true;
		}

		//Apretamos la tecla derecha
		if( (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isLeftArrowPressed){

			OnInputDone.varVector3 = Vector3.right;
			NotificationCenter.defaultCenter.postNotification (OnInputDone);
			isRightArrowPressed = true;
		}

		//Apretamos tecla del salto
		if( Input.GetKeyDown(jumpKey) ){
		
			NotificationCenter.defaultCenter.postNotification (OnJumpPressed);
		}

		//Apretamos tecla de bajar rapido al suelo
		if (Input.GetKeyDown (returnToGround)) {

			NotificationCenter.defaultCenter.postNotification (OnReturnToGround);
		}

		//Soltamos la tecla
		if( (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && isLeftArrowPressed){

			NotificationCenter.defaultCenter.postNotification (OnInputFinished);
			isLeftArrowPressed = false;
		}

		if( (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && isRightArrowPressed){

			NotificationCenter.defaultCenter.postNotification (OnInputFinished);
			isRightArrowPressed = false;
		}

	}
}
