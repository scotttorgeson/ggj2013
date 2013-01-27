using UnityEngine;
using System.Collections;

public class SkipSpawnScript : MonoBehaviour {
	public bool toEnemy = true;
	public int moneyCost;
	
	// Use this for initialization
	void Start () {		
		var gameLoop = (GameLoop)(GameObject.FindObjectOfType(typeof(GameLoop)));
		if ( toEnemy )
			gameLoop.skipEnemySpawn = true;
		else
			gameLoop.skipPlayerSpawn = true;
		//sourcePlayer.currentMoney -= moneyCost;
		GameObject.Destroy(gameObject);
	}
}
