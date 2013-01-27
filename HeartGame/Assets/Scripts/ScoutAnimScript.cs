using UnityEngine;
using System.Collections;

public class ScoutAnimScript : MonoBehaviour {

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
				animation.CrossFade("scout_start_attack");
			else
				animation.CrossFade("scout_cycled_attack");	
		}
		else
		{
			attacking = false;
			animation.CrossFade("scout_move");	
		}	
	}
}
