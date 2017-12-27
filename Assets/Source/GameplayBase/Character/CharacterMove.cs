using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

	[Header ("Movement")]
	public float	groundSpeedIncrese;			//Incremento de velocidad al caminar
	public float	airSpeedIncrease;			//Incremento de velocidad al moverse en el aire
	public float	maxAirSpeed;				//Velocidad maxima en el aire
	public float 	maxGroundSpeed;				//Velocidad maxima en el suelo
	public float 	currentSpeedIncrease;		//Incremento de velocidad actual

	//Movimiento con notificaciones
	[Space (20.0f)]
	public Vector2 movementVector;

	[Header ("Friction")]
	public bool		hasAirInstantStop;			//Se va a parar el movimiento instantaneamente cuando dejemos de movernos en el aire?
	public bool 	hasGroundInstantStop;		//Se va a parar el movimiento instantaneamente cuando dejemos de movernos?
	public float	airFriction;				//La friccion del pj en el aire cuando dejemos de moverlo
	public float	groundFriction;				//Empuje que sufrira el pj cuando dejemos de moverlo

	[Header ("Jump")]
	public float	jumpForce;					//La fuerza del salto
	public int		totalJumps;					//El numero de saltos totales que se pueden hacer antes de tocar el suelo
	public float 	timeBetweenJumps;			//El tiempo minimo entre saltos
	public float	lastJumpTime;				//El tiempo en el que realizo el ultimo salto
	public int		currentJumps;				//El numero se saltos que lleva dados el pj antes de tocar el suelo
	public bool		needJump;					//Se necesita hacer un salto? se marca true cuando se recibe el input
	public bool 	isJumping;					//Esta saltando??
	public bool		isGoingDown;				//Esta bajando?
	public bool		isGoingUp;					//Esta subiendo?
	public bool		isGrounded;					//Esta tocando el suelo?

	[Header ("Checkers")]
	public FloorChecker[]	floorCheckers;		//Referencia a los floor checkers

	private Rigidbody2D 	myRigidbody;		//Referencia al rigidbody

	void Awake () {

		myRigidbody = this.GetComponent<Rigidbody2D> ();
		NotificationCenter.defaultCenter.addListener (OnInputDone, NotificationTypes.oninputdone);
		NotificationCenter.defaultCenter.addListener (OnInputFinished, NotificationTypes.oninputfinished);
		NotificationCenter.defaultCenter.addListener (OnJumpPressed, NotificationTypes.onjumppressed);
	}
		
	public void OnInputDone(Notification note){

		Vector3 vectorValue = (note as Vector3Notification).varVector3;
		movementVector = vectorValue;
	}

	public void OnInputFinished(Notification note){

		movementVector = Vector2.zero;
		StartCoroutine (ApplyFriction());
	}

	public void OnJumpPressed(Notification note){

		//Aplicar mas o menos salto en funcion de la vlocidad maxima que lleve...
		needJump = true;
	}

	void FixedUpdate () {

		Move ();
		GoingUpOrDown ();
		IsCharacterGrounded ();
		Jump ();
	}

	/// <summary>
	/// Detectamos si el personaje esta llendo hacia arriba o hacia abajo
	/// </summary>
	private void GoingUpOrDown(){

		if (myRigidbody.velocity.y < -0.2f) {

			isGoingDown = true;
			isGoingUp = false;
		} else if (myRigidbody.velocity.y > 0.2f) {

			isGoingDown = false;
			isGoingUp = true;
		} else {

			isGoingDown = false;
			isGoingUp = false;
		}
	}

	private void IsCharacterGrounded () {
		
		for (int i = 0; i < floorCheckers.Length; i++) {

			if (floorCheckers [i].IsTouching) {

				isGrounded = true;

				//Si estamos bajando y veniamos de saltar	
				if (!isGoingDown && !isGoingUp) {

					isJumping = false;
					currentJumps = 0;
				}

				return;
			}
		}

		isGrounded = false;
	}

	/// <summary>
	/// Movemos el personaje.
	/// </summary>
	private void Move(){

		//Si el personaje esta en el suelo le damos una velocidad y si está en el aire otra
		if (isGrounded) {

			currentSpeedIncrease = groundSpeedIncrese;
		} else {

			currentSpeedIncrease = airSpeedIncrease;
		}

		//Debug.Log (myRigidbody.velocity.x);

		//Si hacemos un cambio de sentido, nos estabamos moviendo hacia la 
//		if(movementVector == Vector2.left && myRigidbody.velocity.x > 0.0f){
//
//			myRigidbody.velocity = new Vector2(0.0f, myRigidbody.velocity.y);
//
//		} else if(movementVector == Vector2.right && myRigidbody.velocity.x < 0.0f){
//			
//			myRigidbody.velocity = new Vector2(0.0f, myRigidbody.velocity.y);
//		}

		//Debug.Log (movementVector + " . " + myRigidbody.velocity.x);

		//Movemos el pj
		if(movementVector == Vector2.left){

			myRigidbody.AddForce (movementVector * currentSpeedIncrease);

			//Limitamos la velocidad si la superamos
			myRigidbody.velocity = new Vector2 (Mathf.Clamp(myRigidbody.velocity.x, -maxGroundSpeed, 0.0f), myRigidbody.velocity.y);

		} else if(movementVector == Vector2.right){

			myRigidbody.AddForce (movementVector * currentSpeedIncrease);

			//Limitamos la velocidad si la superamos
			myRigidbody.velocity = new Vector2 (Mathf.Clamp(myRigidbody.velocity.x, 0.0f, maxGroundSpeed), myRigidbody.velocity.y);
		}
			
	}

	/// <summary>
	/// Intentamos realizar un salto.
	/// </summary>
	private void Jump(){

		//Si se necesita realizar salto y puede realizar al menos 1
		if(needJump && totalJumps > 0){

			//Primer salto
			if (!isJumping && isGrounded) {

				currentJumps++;
				lastJumpTime = Time.time;

				PerformJump ();
			} else {
				
				//Si tiene saltos disponibles y ha pasado el tiempo minimo para realizar otro salto
				if(currentJumps < totalJumps && timeBetweenJumps <= (Time.time - lastJumpTime)){

					currentJumps++;
					lastJumpTime = Time.time;

					PerformJump ();
				}
			}
		}

		needJump = false;
	}

	/// <summary>
	/// Aplicamos fuerzas al pj par que salte.
	/// </summary>
	private void PerformJump(){

		isJumping = true;
		myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0.0f);
		myRigidbody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
	}

	/// <summary>
	/// Corrutina que aplica friccion 
	/// </summary>
	/// <returns>The friction.</returns>
	private IEnumerator ApplyFriction(){

		//Calculamos la dirccion de movimiento
		bool isMovingRight = false;
		if (Mathf.Abs (myRigidbody.velocity.x) > 0.2f) {

			if (myRigidbody.velocity.x > 0.2f) {

				isMovingRight = true;
			} else if (myRigidbody.velocity.x < -0.2f) {

				isMovingRight = false;
			}
		} else {

#if DEBUG
			Debug.Log ("No direction, el personaje no se puede mover");
#endif
			yield break;
		}

		//Si no estamos en el aire
		if (!isGrounded) {

			if(hasAirInstantStop){

				myRigidbody.velocity = new Vector2 (0.0f, myRigidbody.velocity.y);
				yield break;
			}
		} else if(isGrounded) {

			if(hasGroundInstantStop){
				
				myRigidbody.velocity = new Vector2 (0.0f, myRigidbody.velocity.y);
				yield break;
			}
		}
			
		float currentFriction = 0.0f;

		//Si estamos estamos saltando o en el aire 
		if(isJumping || !isGrounded){

			//Si tenemos parada instantanea en el aire
			if (!hasAirInstantStop) {

				currentFriction = airFriction;
			} else {

				myRigidbody.velocity = new Vector2 (0.0f, myRigidbody.velocity.y);
				yield break;
			}
		} else if(!isJumping && isGrounded){

			//Si tenemos parada instantanea en el suelo
			if (!hasGroundInstantStop) {

				currentFriction = groundFriction;
			} else {

				myRigidbody.velocity = new Vector2 (0.0f, myRigidbody.velocity.y);
				yield break;
			}
		}



		//Frenamos poco a poco
		while (Mathf.Abs(myRigidbody.velocity.x) > 0.0f) {

			//https://stats.stackexchange.com/questions/70801/how-to-normalize-data-to-0-1-range
			float normalizedVelocity = (Mathf.Abs(myRigidbody.velocity.x) - 0.0f)/(maxGroundSpeed - 0.0f);

			if(isMovingRight){

				myRigidbody.AddForce (new Vector2(normalizedVelocity * -currentFriction,0.0f));
			} else {

				myRigidbody.AddForce (new Vector2(normalizedVelocity * currentFriction,0.0f));
			}

			//Si durante el frenazo se pulsa otra tecla se cancea el frenazo
			if (movementVector != Vector2.zero) {

				yield break;
			} else {

				yield return new WaitForEndOfFrame ();
			}
		}
	}

}