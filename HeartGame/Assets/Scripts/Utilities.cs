using UnityEngine;
using System.Collections.Generic;

public static class Utilities {
	public static GameObject[] FindObjectsWithinRange(Vector3 position, string tag, float radius){
		var gobs = GameObject.FindGameObjectsWithTag(tag);
		var retGobs = new List<GameObject>();
		foreach(var gob in gobs){
			if(Vector3.Distance(position, gob.transform.position) <= radius) {
				retGobs.Add (gob);
			}
		}
		return retGobs.ToArray();
	}
}
