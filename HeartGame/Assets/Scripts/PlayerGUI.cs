using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	
	private GameObject selectedSpawner;
	public GameObject selectionRing;
	
	public GUIStyle upgradeButtonStyle;
	public GUIContent upgradeButtonContent;
	public Rect upgradeButtonRect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		UpdateSelection();		
		
		
	}
	
	void OnGUI()
	{
		
		if ( selectedSpawner != null )
		{
			if ( GUI.Button (upgradeButtonRect, upgradeButtonContent, upgradeButtonStyle) )
			{
				// upgrade selectedSpawner
				selectedSpawner.GetComponent<Spawner>().Upgrade();
			}
		}
	}
	
	void UpdateSelection()
	{
		if ( selectedSpawner != null && Utilities.MouseInRect( upgradeButtonRect ) )
				return;
		
		if ( Input.GetButtonDown ("Fire1") )
		{
			var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit rayHit;
			if ( Physics.Raycast( ray, out rayHit, Mathf.Infinity, 1 << 9 ) )
			{
				if ( rayHit.transform.gameObject.GetComponent<Spawner>() != null )
					selectedSpawner = rayHit.transform.gameObject;
			}
			else
				selectedSpawner = null;
		}
		
		if ( selectedSpawner != null )
		{
			selectionRing.transform.position = selectedSpawner.transform.position;	
		}
				
		else
		{
			selectionRing.transform.position = Vector3.zero;
		}
	}
}
