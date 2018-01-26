using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : GunEngine {

	public int 		chargedUsedPerShot;
	public float	proyectileForce;

	protected override void Awake(){

		base.Awake ();
	}

	protected override void Start () {

		base.Start ();
	}
	
	protected override void Update () {

		base.Update ();
	}

	/// <summary>
	//Lanzamos el proyectil 
	/// </summary>
	public void LaunchProyectile(){

		Rigidbody2D newProyectile = Instantiate<Transform> (proyectilePrefab, aimingSystem.weaponPrefab.position, Quaternion.identity).GetComponent<Rigidbody2D>();
		newProyectile.AddForce (aimingSystem.shootingVector * proyectileForce);
	}

#region Notificaciones

	protected override void OnShotPressed(Notification note){

		//Si el arma tiene suficientes cargas como para disparar dispara
		if (CanUseCharge (chargedUsedPerShot)) {

			LaunchProyectile ();
		}
	}

#endregion

}
