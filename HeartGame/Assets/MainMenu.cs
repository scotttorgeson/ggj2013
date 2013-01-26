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
	}
}
