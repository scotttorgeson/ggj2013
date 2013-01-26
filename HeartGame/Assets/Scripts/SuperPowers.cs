using UnityEngine;
using System.Collections;

public class SuperPowers : MonoBehaviour
{
	public GameObject bombSuperPower;
	public string bombAxis;
	public GUIStyle bombButtonStyle;
	public GUIContent bombButtonContent;
	public Rect bombButtonRect;
	public GameObject aimPreview;
	
	private GameObject targetPreview;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetButtonDown( bombAxis )  || (targetPreview !=null && Input.GetButtonDown ("Fire1")))
		{		
			UseBomb();
			if(targetPreview !=null){
				GameObject.Destroy(targetPreview);
				targetPreview = null;
			}
		}
	}
	
	void UseBomb() {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if ( Physics.Raycast( ray, out hit, Mathf.Infinity, 1 << 10 ) )
			{
				Instantiate( bombSuperPower, hit.point, Quaternion.identity );					
			}	
	}
	
	void OnGUI(){
		if(GUI.Button(bombButtonRect, bombButtonContent, bombButtonStyle)){
			targetPreview = (GameObject)Instantiate( aimPreview, Vector3.zero, Quaternion.identity );
		}
		if(targetPreview != null){
			//Draw targetting thingamajig
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if ( Physics.Raycast( ray, out hit, Mathf.Infinity, 1 << 10 ) )
			{
				targetPreview.transform.position = hit.point;			
			}			
		}
	}
}