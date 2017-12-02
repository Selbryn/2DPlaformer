using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour {

	public float	rayDist;			//Distancia a la que lanza el rayo
	public Color	rayColor;			//Color del rayo
	public LayerMask layerMask;			//Mascara de fisicas para el rayo

	private bool	isTouching;			//Esta el rayo tocando algo?

	void Update () {

		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, rayDist, layerMask.value);
		Debug.DrawRay (transform.position, Vector2.down * rayDist, rayColor);
		if (hit.collider != null) {

			isTouching = true;
		} else {

			isTouching = false;
		}
	}

	public bool IsTouching {

		get {
			return isTouching;
		}
	}
}
