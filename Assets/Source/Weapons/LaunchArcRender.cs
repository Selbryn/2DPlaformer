using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class LaunchArcRender : MonoBehaviour {

	public float 	angle;
	public float 	velocity;
	public int 		resolution;

	public Rigidbody2D proyectile;

	private LineRenderer 	lineRend;
	private float 			gravity;
	private float			radianAngle;

	void Awake(){

		lineRend = GetComponent<LineRenderer> ();
		gravity = Mathf.Abs (Physics2D.gravity.y);
	}

	// Use this for initialization
	void Start () {

		RenderArc ();
	}

	void Update(){

		if(Input.GetKeyDown(KeyCode.Space)){

			LaunchProyectile ();
		}
	}

	private void LaunchProyectile (){

		Instantiate<Rigidbody2D> (proyectile, transform.position, Quaternion.identity);
	}

	private void OnValidate(){

		if(lineRend != null && Application.isPlaying){

			RenderArc ();
		}
	}

	private void RenderArc(){

		lineRend.positionCount = resolution;
		lineRend.SetPositions (CalculateArcVector());
	}

	private Vector3[] CalculateArcVector(){

		Vector3[] arcArray = new Vector3[resolution];
		radianAngle = Mathf.Deg2Rad * angle;
		float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / gravity;

		for(int i = 0; i < resolution; i++){
		
			float t = (float)i / (float)resolution;
			arcArray [i] = CalculateArcPoint (t, maxDistance);
		}

		return arcArray;
	}

	private Vector3 CalculateArcPoint (float t, float maxDistance){

		float x = t * maxDistance;
		float y = x * Mathf.Tan (radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
		return new Vector3 (x,y);
	}
}
