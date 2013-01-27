using UnityEngine;
using System.Collections;

public class UnitHealth : MonoBehaviour {
	private int maxHealth;
	private static Vector3 barOffset = new Vector3(0, 5, 1);
	private static Vector3 barSize = new Vector3(40, 10, 0);
	public Texture2D barTexture;
	
	// Use this for initialization
	void Start () {
		var move = gameObject.GetComponent<UnitMovement>();
		if(move!=null){
			maxHealth = move.health;
		}
		
		if(barTexture==null) {
			barTexture = new Texture2D(1,1);
			barTexture.SetPixel(1,1,Color.white);
		}
	}
	
	void OnGUI() {
		var camPos = Camera.main.WorldToScreenPoint(gameObject.transform.position + barOffset) - barSize*0.5f;
		camPos.y = Screen.height - camPos.y;
		var move = gameObject.GetComponent<UnitMovement>();
		if(move.IsVisible() && move.health > 0) {
			var rect = new Rect(camPos.x, camPos.y, barSize.x, barSize.y);
			GUI.color = new Color(1, 0.9f, 0.9f, 0.7f);
			GUI.DrawTexture(rect, barTexture);
			rect.width *= (move.health / maxHealth);
			GUI.color = Color.red;
			GUI.DrawTexture(rect, barTexture);
			GUI.color = Color.white;
		}
	}
}
