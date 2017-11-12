using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour {

	public float	speed;
	public float	distanceToMove;
	public float	endPoint;
	public float	initPoint;

	public bool 	startGoingDown;
	public bool 	isGoingDown;

	private Rigidbody2D myRigidBody;

	void Awake(){

		myRigidBody = GetComponent<Rigidbody2D> ();
	
		if (startGoingDown) {

			isGoingDown = true;
			initPoint = transform.position.y;
			endPoint = initPoint - distanceToMove;
		} else {

			isGoingDown = false;

			initPoint = initPoint + distanceToMove;
			endPoint = transform.position.y;
		}
			
	}

	void FixedUpdate () {

		Vector2 newPosition = Vector2.zero;
		if (isGoingDown) {

			newPosition = new Vector2 (transform.position.x, transform.position.y - speed * Time.fixedDeltaTime);

		} else {

			newPosition = new Vector2 (transform.position.x, transform.position.y + speed * Time.fixedDeltaTime);
		}

		myRigidBody.MovePosition (newPosition);
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
}
