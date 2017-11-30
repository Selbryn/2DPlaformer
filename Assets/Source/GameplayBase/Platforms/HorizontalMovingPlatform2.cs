using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform2 : MonoBehaviour {

	public float	speed;
	public float	distanceToMove;
	public Vector2 	platformDisplacement;

	private float	endPoint;
	private float	initPoint;

	public bool 	startGoingLeft;
	private bool 	isGoingLeft;

	public 	GameObject 	player;
	public	float 		increment;

	void Awake(){
		
		if (startGoingLeft) {

			isGoingLeft = true;
			initPoint = transform.position.x;
			endPoint = initPoint - distanceToMove;
		} else {

			isGoingLeft = false;
		
			endPoint = transform.position.x;
			initPoint = endPoint + distanceToMove;	
		}
			
	}

	void Update () {

		//platformDisplacement = Vector2.zero;
		if (isGoingLeft) {

			platformDisplacement = new Vector2 (-speed * Time.deltaTime, 0.0f);

		} else {

			platformDisplacement = new Vector2 (speed * Time.deltaTime, 0.0f);
		}
			
		transform.Translate(platformDisplacement);
		NeedChangeDirection ();
	}

	private void NeedChangeDirection(){

		if (isGoingLeft) {

			if(transform.position.x <= endPoint){

				isGoingLeft = false;
			}

		} else {

			if(transform.position.x >= initPoint){

				isGoingLeft = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag.ToString() == "Player"){

			Transform collisionTransform = other.transform;

			//Si el que le toca le toca por encima
			if(collisionTransform.position.y >= this.transform.position.y){

				player = other.gameObject;
				player.transform.SetParent (this.transform);
			}
		}
	}

	void OnCollisionExit2D(Collision2D other){

		if(other.gameObject.tag.ToString() == "Player"){

			if(player != null){

				player.transform.SetParent (null);
				player = null;
			}
		}
	}
}
