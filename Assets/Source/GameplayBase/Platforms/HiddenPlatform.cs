using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plataforma que desaparece despues de tocarla.
/// </summary>
public class HiddenPlatform : MonoBehaviour {

	public int 		maxNumOfTouches;
	public float	timeToHide;
	public bool 	canReturn;

	private int 	currentTouches;
	private bool	hidingPlatform;
	private SpriteRenderer	mySpriteRenderer;
	private BoxCollider2D	myBoxCollider2D;

	void Awake(){

		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myBoxCollider2D = GetComponent<BoxCollider2D> ();
	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag.ToString() == "Player"){

			AddTouch ();
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

	}
}
