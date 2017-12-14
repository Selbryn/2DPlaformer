using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EM_GameObject {

	public static Transform FindChildByName(this GameObject go, string childName){

		return go.transform;
	}
}
