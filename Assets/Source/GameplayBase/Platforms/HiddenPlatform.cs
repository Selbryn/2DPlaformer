using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plataforma que desaparece despues de tocarla.
/// </summary>
public class HiddenPlatform : MonoBehaviour {

	public int 		maxNumOfTouches;		//Los numeros de toques que tiene disponble antes de ocultarse
	public float	timeToHide;				//Tiempo desde que se pisa hasta que se oculta
	public bool 	canReturn;				//Puede volver a colocarse?
	public float	timeToReturn;			//Tiempo que tiene que pasar hasta que vuelva a colocarse

	private int 	currentTouches;			//El numero de toques que esstan acumulados
	private bool	hidingPlatform;			//Se está ocultando la plataforma
	private SpriteRenderer	mySpriteRenderer;	//Sprite renderer
	private BoxCollider2D	myBoxCollider2D;	//Collider 2d

	void Awake(){

		//Cacheamos componentes
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myBoxCollider2D = GetComponent<BoxCollider2D> ();
	}

	void OnCollisionEnter2D(Collision2D other){

		//Si colisiona el player con la plataforma
		if(other.gameObject.tag.ToString() == "Player"){

			Transform collisionTransform = other.transform;

			//Si el que le toca le toca por encima
			if(collisionTransform.position.y >= this.transform.position.y){

				AddTouch ();
			}
		}
	}

	private void AddTouch(){

		if(currentTouches < maxNumOfTouches){

			currentTouches++;
		}

		CheckIfNeedsHide ();
	}

	private void CheckIfNeedsHide(){

		if(currentTouches >= maxNumOfTouches){

			if(!hidingPlatform){

				StartCoroutine(HidePlatform ());
			}
		}
	}

	private IEnumerator HidePlatform(){

		hidingPlatform = true;

		yield return new WaitForSeconds (timeToHide);

		mySpriteRenderer.enabled = false;
		myBoxCollider2D.enabled = false;

		if(canReturn){

			StartCoroutine (EnablePlatform());
		}
	}

	private IEnumerator EnablePlatform(){

		yield return new WaitForSeconds (timeToReturn);

		hidingPlatform = false;
		mySpriteRenderer.enabled = true;
		myBoxCollider2D.enabled = true;

	}

}
