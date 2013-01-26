using UnityEngine;
using System.Collections;

public static class Utilities {
	public static GameObject[] FindObjectsWithinRange(Vector3 position, string tag, float radius){
		var gobs = Physics.OverlapSphere(positoin, radius);
		var retGobs = new Generic.List<GameObject>();
		foreach(var gob in gobs){
			if(gob.CompareTag(tag)){
				retGobs.Add (gob);
			}
		}
		return retGobs.ToArray();
	}
}
