using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Draw the GUI
	void OnGUI () {
		GUI.Box (new Rect(10,10,100,90), "Main Menu");
		
		if(GUI.Button (new Rect(20,40,80,20), "Start Game")){
			Application.LoadLevel(1);
		}
		
		var RightAlign = new GUIStyle(){
			alignment = TextAnchor.MiddleRight,
		};
		
		var LeftAlign = new GUIStyle(){
			alignment = TextAnchor.MiddleLeft
		};
		LeftAlign.normal.textColor = Color.white;
		LeftAlign.active.textColor = Color.white;
		
		GUILayout.BeginArea(new Rect(Screen.width - 220, 10, 200, Screen.height - 20));
		GUILayout.BeginVertical();
		
		GUILayout.Label("CREDITS");
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Scott Torgesen", LeftAlign);
		GUILayout.Label("Programmer", RightAlign);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Gil Bojorquez", LeftAlign);
		GUILayout.Label ("Artist", RightAlign);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Kyle Chittenden", LeftAlign);
		GUILayout.Label ("Artist", RightAlign);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Andrew Jones", LeftAlign);
		GUILayout.Label ("Programmer", RightAlign);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Eric Rios", LeftAlign);
		GUILayout.Label ("Artist", RightAlign);
		GUILayout.EndHorizontal();
		
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
