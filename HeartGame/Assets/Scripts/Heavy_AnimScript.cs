using UnityEngine;
using System.Collections;

public class Heavy_AnimScript : MonoBehaviour {
	bool attacking = false;
	float playStartAttackUntil = 0.0f;
	
	void Update () {
		if ( gameObject.transform.parent.GetComponent<UnitMovement>().attacking )
		{
			
			if (attacking == false )
			{
				attacking = true;
				playStartAttackUntil = Time.time + 0.8f;
			} 
			
			if ( Time.time < playStartAttackUntil )
				animation.CrossFade("heavy_attack_start");
			else
				animation.CrossFade("heavy_attack");
		}
		else
		{
			animation.CrossFade("heavy_walk");	
		}
	}
}
