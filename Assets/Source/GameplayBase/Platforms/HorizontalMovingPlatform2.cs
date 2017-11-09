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
	public 	Rigidbody2D player;
	public	float 		increment;

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

	void FixedUpdate(){

		if(player != null){

			//player.MovePosition (new Vector2(player.transform.position.x + platformDisplacement.x, player.transform.position.y + platformDisplacement.y));
			player.transform.Translate(platformDisplacement * increment * Time.fixedDeltaTime);
		}
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

		Debug.Log ("Other:" + other.transform.name);
		player = other.gameObject.GetComponent<Rigidbody2D> ();
	}

	void OnCollisionExit2D(Collision2D other){

		Debug.Log ("Other:" + other.transform.name);
		player = null;
	}
}
