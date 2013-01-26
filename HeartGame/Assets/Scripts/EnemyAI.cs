using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var enemyBase = gameObject.GetComponent<PlayerScript>();
		foreach(var spawn in enemyBase.spawnPoints){
			if(spawn.pathIndex < spawn.pathes.Count) {
				if(enemyBase.currentMoney >= enemyBase.upgradeCosts[spawn.tier] && spawn.tier < spawn.unitSpawns.Count - 1) {
					enemyBase.currentMoney -= enemyBase.upgradeCosts[spawn.tier];
					spawn.Upgrade();
				}				
			}
		}
	}
}
