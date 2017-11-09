using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour {

	public float	speed;
	public float	distanceToMove;

	private float	endPoint;
	private float	initPoint;

	public bool 	startGoingLeft;
	private bool 	isGoingLeft;

	private Rigidbody2D myRigidBody;

	void Awake(){

		myRigidBody = GetComponent<Rigidbody2D> ();
	
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

	void FixedUpdate () {

		Vector2 newPosition = Vector2.zero;
		if (isGoingLeft) {

			newPosition = new Vector2 (transform.position.x  - speed * Time.fixedDeltaTime, transform.position.y);

		} else {

			newPosition = new Vector2 (transform.position.x + speed * Time.fixedDeltaTime, transform.position.y);
		}

		myRigidBody.MovePosition (newPosition);
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
}
