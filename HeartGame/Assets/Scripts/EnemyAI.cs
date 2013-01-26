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
				//NOTE: Ugly, hackish, ineffective - do better if possible
				if(spawn.currentUpgrade.upgrades !=null && spawn.currentUpgrade.upgrades.Length > 0){
					var i = Random.Range (0, spawn.currentUpgrade.upgrades.Length-1);
					if(enemyBase.currentMoney >= spawn.currentUpgrade.upgrades[i].cost) {
						enemyBase.Upgrade(spawn, spawn.currentUpgrade.upgrades[i]);
					}
				}			
			}
		}
	}
}
