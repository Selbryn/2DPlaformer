using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform1 : MonoBehaviour {

	public float	speed;
	public float	distanceToMove;
	public float	endPoint;
	public float	initPoint;

	public bool 	startGoingLeft;
	public bool 	isGoingLeft;

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

			//newPosition = new Vector2 (transform.position.x  - speed * Time.fixedDeltaTime, transform.position.y);
			newPosition = new Vector2 (-speed * Time.fixedDeltaTime, 0.0f);

		} else {

			//newPosition = new Vector2 (transform.position.x + speed * Time.fixedDeltaTime, transform.position.y);
			newPosition = new Vector2 (speed * Time.fixedDeltaTime, 0.0f);
		}

		//myRigidBody.MovePosition (newPosition);
		myRigidBody.velocity = new Vector2(newPosition.x, newPosition.y);
		NeedChangeDirection ();
	}

	private void NeedChangeDirection(){

		if (isGoingLeft) {

			if(transform.position.x <= endPoint){

				isGoingLeft = false;
				myRigidBody.velocity = Vector2.zero;
			}

		} else {

			if(transform.position.x >= initPoint){

				isGoingLeft = true;
				myRigidBody.velocity = Vector2.zero;
			}
		}
	}
}
