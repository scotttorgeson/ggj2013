using UnityEngine;
using System.Collections;

public class FlyingUnit1_AnimScript : MonoBehaviour
{
	private string fly1 = "sleighter_fly";
	private string fly2 = "sleighter_fly2";
	private string attack = "sleighter_attack";
	
	private float duration = 1.0f;
	private bool whichFly = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( gameObject.transform.parent.GetComponent<UnitMovement>().attacking )
		{
			animation.CrossFade( attack );
		}
		else
		{
			if ( whichFly )
				animation.CrossFade( fly1 );
			else
				animation.CrossFade( fly2 );
		}
		
		duration -= Time.deltaTime;
		
		if ( duration < 0.0f )
		{
			duration = Random.Range( 1.0f, 5.0f );	
		}
	}
}
