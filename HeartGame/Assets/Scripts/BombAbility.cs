using UnityEngine;
using System.Collections;

public class BombAbility : MonoBehaviour {
	public float bombRadius;
	public GameObject player;
	public int bombDamage;
	
	// Use this for initialization
	void Start () {
		//Find everything in the radius
		var targets = Utilities.FindObjectsWithinRange(gameObject.transform.position, "Creep", bombRadius);
		
		foreach(var target in targets){
			//TODO: Filter out player's own units
			//TODO: Do damage
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameObject.particleSystem.isPlaying){
			GameObject.Destroy(gameObject);
		}
	}
}
