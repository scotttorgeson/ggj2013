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
	
	public static bool MouseInRectGUI(Rect rect)
	{
		return rect.Contains( TranslateMouseForGUI() );
	}
	
	public static Vector3 TranslateMouseForGUI()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.y = Screen.height - mousePosition.y;
		return mousePosition;
	}
}
