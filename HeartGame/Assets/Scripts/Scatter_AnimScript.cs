using UnityEngine;
using System.Collections;

public class Scatter_AnimScript : MonoBehaviour {

	void Update () {
		if ( gameObject.transform.parent.GetComponent<UnitMovement>().attacking )
		{
			animation.CrossFade("skatterbot_attack");	
		}
		else
		{
			animation.CrossFade("skatterbot_fly");	
		}
	}
}
