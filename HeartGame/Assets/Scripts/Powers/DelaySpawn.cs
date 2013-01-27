using UnityEngine;
using System.Collections;

public class DelaySpawn : MonoBehaviour {
	public bool targetPlayer = false;
	
	// Use this for initialization
	void Start () {
		GameLoop target = (GameLoop)GameObject.FindObjectOfType(typeof(GameLoop));
		if(targetPlayer)
			target.SkipPlayerSpawn();
		else
			target.SkipEnemySpawn();
	}
}
