using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	Vector3 target;
	bool targetSet = false;
	public float speed = 100.0f;
	
	public void SetTarget(Vector3 target)
	{
		this.target = target;
		targetSet = true;
	}
	
	// Use this for initialization
	void Start () {
		GameObject.Destroy( gameObject, 3.0f );
	}
	
	// Update is called once per frame
	void Update () {		
		if ( targetSet == false )
		{
			transform.position += transform.forward.normalized * speed * Time.deltaTime;
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
			if ( Vector3.Distance(transform.position, target) < 1.0f )
				GameObject.Destroy( gameObject );
		}
	}
}
