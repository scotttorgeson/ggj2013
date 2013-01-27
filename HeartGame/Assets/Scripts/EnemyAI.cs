using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {	
	void Start() {
	}
	void Update() {
		var enemyBase = gameObject.GetComponent<PlayerScript>();
		foreach(var spawn in enemyBase.spawnPoints){
			if(spawn.pathIndex <= spawn.pathes.Count-1){
				if(spawn.currentUpgrade !=null &&
					spawn.currentUpgrade.upgrades.Length > 0 &&
					spawn.currentUpgrade.upgrades[0].cost <= enemyBase.currentMoney){
					enemyBase.Upgrade(spawn, spawn.currentUpgrade.upgrades[0]);
				}
			}
		}
	}
}
