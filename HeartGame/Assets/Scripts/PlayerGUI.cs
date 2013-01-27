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
	public Vector2 mapSize = new Vector2 (256, 384);
	public Rect sceneSize = new Rect (-525, 235, 1024, 1536);
	public float iconSize = 8;
	public Color enemyColor = Color.red;
	public Color friendlyColor = Color.blue;
	public Texture iconTexture;
	
	private Rect mapRect;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		mapRect = new Rect (Screen.width - mapSize.x - 10, Screen.height - mapSize.y - 10, mapSize.x, mapSize.y);
		UpdateSelection ();
	}
	
	Vector2 getMapPos (Vector3 worldPos)
	{
		Vector3 absMapPos = new Vector3 ((worldPos.x - sceneSize.x)/sceneSize.width - 0.5f,(worldPos.z - sceneSize.y) / sceneSize.height - 0.5f, 0);
		absMapPos = Quaternion.AngleAxis(-40, Vector3.up) * absMapPos;
		absMapPos += new Vector3(0.5f, 0, 0.5f);
		absMapPos = new Vector3 (absMapPos.x * sceneSize.width, absMapPos.z * sceneSize.height, 0);
		return absMapPos;
	}
	
	Vector3 getWorldPos (Vector2 mapPos){
		Vector3 tmp = new Vector3(mapPos.x / mapRect.width - 0.5f, 0, 
			mapPos.y / mapRect.height - 0.5f);
		tmp = Quaternion.AngleAxis(40, Vector3.up) * tmp;
		tmp = new Vector3((tmp.x + 0.5f) * sceneSize.width + sceneSize.x, 0,
			(tmp.y+0.5f) * sceneSize.height + sceneSize.y);
		return tmp;
	}
	
	void OnGUI ()
	{		
		GUI.BeginGroup (mapRect, mapStyle);
		var icons = GameObject.FindGameObjectsWithTag ("EnemyUnit");
		GUI.color = Color.red;
		foreach (var icon in icons) {
			var mapPos = getMapPos (icon.transform.position);
			var unitMove = icon.GetComponent<UnitMovement>();
			if(unitMove!=null && unitMove.IsVisible()){
				GUI.DrawTexture (new Rect (mapPos.x, mapPos.y, iconSize, iconSize),
					iconTexture);
			}
		}
				
		icons = GameObject.FindGameObjectsWithTag ("PlayerUnit");
		GUI.color = Color.blue;
		foreach (var icon in icons) {
			var mapPos = getMapPos (icon.transform.position);
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
		
		GUI.color = Color.white;
		
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
			if(Utilities.MouseInRectGUI(mapRect)) {
				var target = getWorldPos((Vector2)Utilities.TranslateMouseForGUI() - new Vector2(mapRect.x, mapRect.y));
				//Yay - time for magic numbers
				Camera.main.transform.position = target + new Vector3(186, 358, -48);
			}
			else{
				var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit rayHit;
				if (Physics.Raycast (ray, out rayHit, Mathf.Infinity, 1 << 9)) {
					if (rayHit.transform.gameObject.GetComponent<Spawner> () != null)
						selectedSpawner = rayHit.transform.gameObject;
				} else
					selectedSpawner = null;
			}
		}
		
		if (selectedSpawner != null) {
			selectionRing.transform.position = selectedSpawner.transform.position;	
		} else {
			selectionRing.transform.position = Vector3.zero;
		}
	}
}
