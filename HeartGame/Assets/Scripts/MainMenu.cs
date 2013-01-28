using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	float aiDifficulty = 0;
	public Texture menuBackground;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Draw the GUI
	void OnGUI () {
		GUI.color = Color.white;
		GUI.Box ( new Rect(0, 0, Screen.width, Screen.height), menuBackground);
		GUI.Box (new Rect(10,10,100,160), "Main Menu");
		
		if(GUI.Button (new Rect(20,40,80,20), "Start Game")){
			Application.LoadLevel(2);
		}
		
		if(GUI.Button (new Rect(20, 80, 80, 20), "Demo Mode")){
			Application.LoadLevel(1);
		}
		
		GUI.Label(new Rect(20, 120, 80, 20), "AI Difficulty");
		aiDifficulty = GUI.HorizontalSlider(new Rect(20, 110, 80, 20),
			aiDifficulty, 0, 3);
		if(aiDifficulty<0.7){
			GUI.color = Color.green;
			GUI.Label(new Rect(20, 170, 80, 20), "Easy");
			EnemyAI.difficulty = 0;
		}
		else if(aiDifficulty<1.4){
			GUI.color = new Color(1, 0.4f, 0);
			GUI.Label (new Rect(20, 170, 80, 20), "Medium");
			EnemyAI.difficulty = 1;
		}
		else{
			GUI.color = Color.red;
			GUI.Label (new Rect(20, 170, 80, 20), "Hard");
			EnemyAI.difficulty = 2;
		}
		GUI.color = Color.white;
		
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
