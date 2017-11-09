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

		platformDisplacement = Vector2.zero;
		if (isGoingLeft) {

			platformDisplacement = new Vector2 (-speed * Time.fixedDeltaTime, 0.0f);

		} else {

			platformDisplacement = new Vector2 (speed * Time.fixedDeltaTime, 0.0f);
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
}
