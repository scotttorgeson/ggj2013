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
	public int cost = 250;
	public PlayerScript source;
	
	private GameObject targetPreview;
	
	// Use this for initialization
	void Start ()
	{
		bombButtonRect.y = Screen.height - 20 - bombButtonRect.height;		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetButtonDown( bombAxis )  || (targetPreview != null && Input.GetButtonDown ("Fire1")))
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if ( Physics.Raycast( ray, out hit, Mathf.Infinity, 1 << 10 ) )
			{
				UsePower( source, bombSuperPower, cost, hit.point );					
			}
			
			if(targetPreview !=null){
				GameObject.Destroy(targetPreview);
				targetPreview = null;
			}
		}
	}
	
	public static void UsePower(PlayerScript sourcePlayer, GameObject power, int powerCost, Vector3 position)
	{
		if(sourcePlayer.currentMoney >= powerCost)
		{
			sourcePlayer.currentMoney -= powerCost;
			Instantiate( power, position, Quaternion.identity );
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