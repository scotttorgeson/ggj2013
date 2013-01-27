using UnityEngine;
using System.Collections;

public class Regular_AnimScript : MonoBehaviour {	
	bool attacking = false;
	float playStartAttackUntil = 0.0f;
	
	void Update () {
		if ( gameObject.transform.parent.GetComponent<UnitMovement>().attacking )
		{
			if (attacking == false )
			{
				attacking = true;
				playStartAttackUntil = Time.time + 0.66f;
			} 
			
			if ( Time.time < playStartAttackUntil )
				animation.CrossFade("regular_attack_start");
			else
				animation.CrossFade("regular_attack");
		}
		else
		{
			animation.CrossFade("regular_walk");	
		}
	}
}
