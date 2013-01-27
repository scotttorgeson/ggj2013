using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
	
	private GameObject selectedSpawner;
	public GameObject selectionRing;
	public GUIStyle upgradeButtonStyle;
	public GUIContent upgradeButtonContent;
	public Rect upgradeButtonRect;
	public GUIStyle resourceTextStyle;
	public Rect resourceTextRect;
	public GUIStyle mapStyle;
	public Vector2 mapSize = new Vector2 (128, 512);
	public Rect sceneSize = new Rect (-250, 225, 400, 1575);
	public float iconSize = 3;
	public Color enemyColor;
	public Color friendlyColor;
	public Texture iconTexture;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateSelection ();
	}
	
	Vector2 getMapPos (Rect mapRect, Vector3 worldPos)
	{
		return new Vector2 ((worldPos.x - sceneSize.x) * mapRect.width / sceneSize.width,
			mapRect.height - (worldPos.z - sceneSize.y) * mapRect.height / sceneSize.height);
	}
	
	void OnGUI ()
	{
		var mapRect = new Rect (Screen.width - mapSize.x - 10, Screen.height - mapSize.y - 10, mapSize.x, mapSize.y);
		GUI.BeginGroup (mapRect, mapStyle);
		var icons = GameObject.FindGameObjectsWithTag ("EnemyUnit");
		GUI.color = Color.red;
		foreach (var icon in icons) {
			var mapPos = getMapPos (mapRect, icon.transform.position);
			var unitMove = icon.GetComponent<UnitMovement>();
			if(unitMove!=null && unitMove.IsVisible()){
				GUI.DrawTexture (new Rect (mapPos.x, mapPos.y, iconSize, iconSize),
					iconTexture);
			}
		}
				
		icons = GameObject.FindGameObjectsWithTag ("PlayerUnit");
		GUI.color = Color.blue;
		foreach (var icon in icons) {
			var mapPos = getMapPos (mapRect, icon.transform.position);
			GUI.DrawTexture (new Rect (mapPos.x, mapPos.y, iconSize, iconSize),
				iconTexture);
		}
		GUI.EndGroup ();
					
		if (selectedSpawner != null) {
			var spawner = selectedSpawner.GetComponent<Spawner> ();
			if (spawner != null && spawner.currentUpgrade != null) {
				//display upgrade path selection
				foreach (var upgrade in spawner.currentUpgrade.upgrades) {
					if (GUI.Button (upgrade.upgradeButtonRect, upgrade.upgradeButtonContent, upgrade.upgradeButtonStyle)) {
						gameObject.GetComponent<PlayerScript> ().Upgrade (spawner, upgrade);
					}
				}
			}
		}
		
		int resourceCount = gameObject.GetComponent<PlayerScript> ().currentMoney;
		GUI.TextArea (resourceTextRect, resourceCount.ToString (), resourceTextStyle);
	}
	
	void UpdateSelection ()
	{
		if (selectedSpawner != null) {
			var spawner = selectedSpawner.GetComponent<Spawner> ();
			//display upgrade path selection
			foreach (var upgrade in spawner.currentUpgrade.upgrades) {
				if (Utilities.MouseInRectGUI (upgrade.upgradeButtonRect)) 
					return;
			}
		}
		
		if (Input.GetButtonDown ("Fire1")) {
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rayHit;
			if (Physics.Raycast (ray, out rayHit, Mathf.Infinity, 1 << 9)) {
				if (rayHit.transform.gameObject.GetComponent<Spawner> () != null)
					selectedSpawner = rayHit.transform.gameObject;
			} else
				selectedSpawner = null;
		}
		
		if (selectedSpawner != null) {
			selectionRing.transform.position = selectedSpawner.transform.position;	
		} else {
			selectionRing.transform.position = Vector3.zero;
		}
	}
}
