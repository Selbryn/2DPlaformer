using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour {

	public float	speed;
	public float	distanceToMove;
	public Vector2 	platformDisplacement;

	private float	endPoint;
	private float	initPoint;

	public bool 	startGoingDown;
	private bool 	isGoingDown;

	public 	GameObject 	player;

	void Awake(){
	
		if (startGoingDown) {

			isGoingDown = true;
			initPoint = transform.position.y;
			endPoint = initPoint - distanceToMove;
		} else {

			isGoingDown = false;

			endPoint = transform.position.y;
			initPoint = endPoint + distanceToMove;
		}
			
	}

	void Update () {

		if (isGoingDown) {

			platformDisplacement = new Vector2 (0.0f,- speed * Time.deltaTime);

		} else {

			platformDisplacement = new Vector2 (0.0f,+ speed * Time.deltaTime);
		}

		transform.Translate (platformDisplacement);
		NeedChangeDirection ();

	}

	private void NeedChangeDirection(){

		if (isGoingDown) {

			if(transform.position.y <= endPoint){

				isGoingDown = false;
			}

		} else {

			if(transform.position.y >= initPoint){

				isGoingDown = true;
			}
		}
	}

	/// <summary>
	/// Cuando algo colisiona contra la plataforma.
	/// </summary>
	void OnCollisionEnter2D(Collision2D other){

		//Si es el jugador
		if(other.gameObject.tag.ToString() == "Player"){

			Transform collisionTransform = other.transform;

			//Si el que le toca le toca por encima
			if(collisionTransform.position.y >= this.transform.position.y){

				//Lo añadimos como hijo y lo trackeamos
				player = other.gameObject;
				player.transform.SetParent (this.transform);
			}
		}
	}

	/// <summary>
	/// Cuando la colision se pierde
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionExit2D(Collision2D other){

		//Si es el jugador
		if(other.gameObject.tag.ToString() == "Player"){

			//Lo sacamos de los hijos
			if(player != null){

				player.transform.SetParent (null);
				player = null;
			}
		}
	}
}
