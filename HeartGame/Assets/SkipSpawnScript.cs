using UnityEngine;
using System.Collections;

public class SkipSpawnScript : MonoBehaviour {
	public GameObject sourcePlayer;
	public GameObject targetPlayer;
	public int moneyCost;
	
	// Use this for initialization
	void Start () {		
		var gameLoop = (GameLoop)GameObject.FindObjectOfType(typeof(GameLoop));
		gameLoop.skipEnemySpawn = true;
		//sourcePlayer.currentMoney -= moneyCost;
		GameObject.Destroy(gameObject);
	}
}
