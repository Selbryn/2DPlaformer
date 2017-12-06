using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingSystem : MonoBehaviour { 

	public 	float		peedholeDistance;	//Distancia minima de la mirilla 		
	public  float		weaponDistance;

	public 	Transform 	peepHolePrefab;		//La mirilla
	public 	Transform 	weaponPrefab;		//El arma
	public	Transform 	player;			//El jugador

	public	Vector2		shootingVector;	//El vector de disparo actual

	private Camera 		mainCam;		//La camara principal

	void Awake () {

		mainCam = Camera.main;
	}
	
	void Update () {

		PaintPeehole ();
	}

	private void PaintPeehole(){
		
		Vector3 mouseVectorPosition = mainCam.ScreenToWorldPoint (Input.mousePosition);
		Vector3 peepholeNewPosition = new Vector3(mouseVectorPosition.x, mouseVectorPosition.y, 0.0f);
		Vector3 peepholeNormPosition = (peepholeNewPosition - player.position).normalized;

#if DEBUG
		Debug.DrawLine (player.position, peepholeNewPosition, Color.green);
		Debug.DrawRay (player.position, (peepholeNewPosition - player.position).normalized * peedholeDistance, Color.red);
#endif
		//Si la magnitud del peephole es mayor que la magnitud de la distancia normalizada 
		if ((peepholeNewPosition - player.position).magnitude > ((peepholeNewPosition - player.position).normalized * peedholeDistance).magnitude) {

			peepHolePrefab.position = peepholeNewPosition;
		} else {

			peepHolePrefab.position = player.position + (peepholeNormPosition * peedholeDistance);
		}

		weaponPrefab.position = player.position + (peepholeNormPosition * weaponDistance);

		shootingVector = peepholeNormPosition;
	}
}
