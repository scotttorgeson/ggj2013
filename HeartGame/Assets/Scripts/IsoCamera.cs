using UnityEngine;
using System.Collections;

public class IsoCamera : MonoBehaviour {
	
	public float scrollSpeed = 10.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float camX = Input.GetAxis("Horizontal");
		float camY = Input.GetAxis("Vertical");
		
		if ( Input.mousePosition.x <= 1 )
			camX = -1.0f;
		if ( Input.mousePosition.x >= Screen.width - 1 )
			camX = 1.0f;
		if ( Input.mousePosition.y <= 1 )
			camY = -1.0f;
		if ( Input.mousePosition.y >= Screen.height - 1 )
			camY = 1.0f;
		
		
		Vector3 forward = this.gameObject.transform.forward;
		forward.y = 0.0f;
		forward.Normalize();
		
		Vector3 right = this.gameObject.transform.right;
		right.y = 0.0f;
		right.Normalize();
		
		this.gameObject.transform.position += forward * camY * Time.deltaTime * scrollSpeed;
		this.gameObject.transform.position += right * camX * Time.deltaTime * scrollSpeed;
	}
}
