using UnityEngine;
using System.Collections;

public class BaseHealth : MonoBehaviour {
	private int maxHealth = 0;
	private static Vector3 barOffset = new Vector3(0, 5, 1);
	private static Vector3 barSize = new Vector3(80, 10, 0);
	public Texture2D barTexture;
	
	// Use this for initialization
	void Start () {
		
		if(barTexture==null) {
			barTexture = new Texture2D(1,1);
			barTexture.SetPixel(1,1,Color.white);
		}
	}
	
	void OnGUI() {
		if(maxHealth<=0)
			maxHealth = gameObject.GetComponent<PlayerScript>().currentLife;
		
		var camPos = Camera.main.WorldToScreenPoint(gameObject.transform.position + barOffset) - barSize*0.5f;
		camPos.y = Screen.height - camPos.y;
		var move = gameObject.GetComponent<PlayerScript>();
		
		if(move.currentLife>0){
			var rect = new Rect(camPos.x, camPos.y, barSize.x, barSize.y);
			GUI.color = new Color(1, 0.9f, 0.9f, 0.7f);
			GUI.DrawTexture(rect, barTexture);
			rect.width *= (move.currentLife / maxHealth);
			GUI.color = Color.red;
			GUI.DrawTexture(rect, barTexture);
			GUI.color = Color.white;
		}
	}
}
