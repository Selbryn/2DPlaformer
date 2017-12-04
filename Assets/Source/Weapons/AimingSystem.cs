using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingSystem : MonoBehaviour { 

	public 	float		peedholeDistance;			

	public 	Transform 	aimTestSprite;
	public	Transform 	player;	

	private Camera 		mainCam;

	// Use this for initialization
	void Awake () {

		mainCam = Camera.main;
	}
	
	// Update is called once per frame
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

			aimTestSprite.position = peepholeNewPosition;
		} else {

			aimTestSprite.position = player.position + (peepholeNormPosition * peedholeDistance);
		}
			
	}
}
